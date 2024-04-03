using IntegrationEvents;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public class ProductCreatedFunction
    {
        private const string _queueName = nameof(ProductCreatedIntegrationEvent);
        private const string _connectionStringSetting = "RabbitMQConnection";

        private readonly ILogger _logger;

        public ProductCreatedFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProductCreatedFunction>();
        }

        [Function(nameof(ProductCreatedFunction))]
        [RabbitMQOutput(QueueName = _queueName, ConnectionStringSetting = _connectionStringSetting)]
        public static string Run(
            [RabbitMQTrigger(_queueName, ConnectionStringSetting = _connectionStringSetting)]
            string item,
            FunctionContext context)
        {
            var logger = context.GetLogger(nameof(ProductCreatedFunction));

            logger.LogInformation(item);

            var message = $"Output message created at {DateTime.Now}";
            return message;
        }
    }
}
