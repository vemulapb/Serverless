# Serverless Application



![This is an alt text.](/Serverless.svg)


There are 2 AWS Lambda functions. 
* SQSPublisher which is triggered on HTTP Post. The lambda takes the JSON payload containing book data and writes to SQS Queue. 
* SQSConsumer is triggered when messages arrive on the SQS Queue. The lambda stores book data contained in the message in DynamoDB. 


## Here are some steps to follow from Visual Studio:

* To deploy your function to AWS Lambda, right click the project in Solution Explorer and select *Publish to AWS Lambda*.

* To view your deployed function open its Function View window by double-clicking the function name shown beneath the AWS Lambda node in the AWS Explorer tree.

* To perform testing against your deployed function use the Test Invoke tab in the opened Function View window.

### Here are a few things that have to be done through the Amazon console after the Lambdas have been deployed:

* Create an API through the API Gateway for a Post method. Setup the trigger to SQSPublisher Lambda

* Create a SQS Queue called ProductQueue. Setup the trigger to SQSConsumer Lambda

* Creata DynamoDB table called ProductCatalog

* Through IAM create a policy called lambda-custom-policy and grant Read, Write access to SQSQueues and DynamoDB

* Create a role lambda-apigateway-role and attach the above policy to this role.




