using Amazon.Lambda.Core;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SQSPublisher;

public class Function
{
    private static readonly string queueUrl = Environment.GetEnvironmentVariable("SQS_QUEUE_URL") 
        ?? throw new InvalidOperationException("SQS_QUEUE_URL environment variable is not set");
    private static readonly IAmazonSQS sqsClient = new AmazonSQSClient();
    
    public async Task<string> FunctionHandler(Book book, ILambdaContext context)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));
            
        if (string.IsNullOrWhiteSpace(book.Id))
            throw new ArgumentException("Book.Id is required");
        
        try
        {
            string jsonMessage = JsonSerializer.Serialize(book);
            return await SendMessage(sqsClient, queueUrl, jsonMessage);
        }
        catch (Exception ex)
        {
            context.Logger.LogLine($"Error publishing message: {ex.Message}");
            throw;
        }
    }

    private static async Task<string> SendMessage(
      IAmazonSQS sqsClient, string qUrl, string messageBody)
    {
        SendMessageResponse responseSendMsg =
          await sqsClient.SendMessageAsync(qUrl, messageBody);
    
        return responseSendMsg.HttpStatusCode.ToString();
    }

    public class Book
    {        
        public string Id { get; set; }
  
        public string Title { get; set; }
        
        public int Price { get; set; }
        
        public string ISBN { get; set; }
       
        public List<string> Authors { get; set; }
    }


}
