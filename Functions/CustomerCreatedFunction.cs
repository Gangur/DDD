using Domain.Customers;
using Domain.OutboxMessage;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Rebus.Messages;

namespace Functions
{
    public class CustomerCreatedFunction
    {
        private readonly ILogger _logger;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CustomerCreatedFunction(ILoggerFactory loggerFactory, IOutboxMessageRepository outboxMessageRepository)
        {
            _logger = loggerFactory.CreateLogger<CustomerCreatedFunction>();
            _outboxMessageRepository = outboxMessageRepository;
        }

        [Function(nameof(CustomerCreatedFunction))]
        public async Task Run([TimerTrigger("*/20 * * * * *")] TimerInfo myTimer, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            var messages = await _outboxMessageRepository.ResiveUnprocessedMessagesAsync(20, 
                typeof(CustomerCreatedDomainEvent).Name, CancellationToken.None);

            for(int i = 0; i < messages.Count; i++)
            {
                _logger.LogInformation($"Processed outbox message {messages[i].Type} {messages[i].Id}");
                await _outboxMessageRepository.CompleteAsync(messages[i], CancellationToken.None);
            }
        }
    }
}
