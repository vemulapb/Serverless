using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

internal class Program
{
    private static void Main(string[] args)
    {
        var credentials = new BasicAWSCredentials("accessKey", "secretKey");
        var client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
        
    }
}