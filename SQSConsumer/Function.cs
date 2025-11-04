using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using System.Text.Json;
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SQSConsumer;

public class Function
{
    private readonly AmazonDynamoDBClient _dynamoDbClient;
    private readonly DynamoDBContext _dynamoDbContext;

    public Function()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
        _dynamoDbContext = new DynamoDBContext(_dynamoDbClient);
    }

    
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            Book book = JsonSerializer.Deserialize<Book>(message.Body);
            book.Id = Guid.NewGuid().ToString();

            await _dynamoDbContext.SaveAsync(book);
        }
    }


    [DynamoDBTable("ProductCatalog")]  
    public class Book
    {
        [DynamoDBHashKey]  // Partition key
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public int Price { get; set; }

        [DynamoDBProperty]
        public string ISBN { get; set; }

        [DynamoDBProperty]
        public List<string> Authors { get; set; }
    }
}
