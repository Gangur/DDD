using EmailsSender.Abstractions;
using IntegrationEvents;
using MassTransit;

namespace EmailsSender.Consumers
{
    public sealed class CustomerDeletedEventConsumer : IConsumer<CustomerDeletedIntegrationEvent>
    {
        public readonly IEmailSender _emailSender;

        public CustomerDeletedEventConsumer()
        {

        }

        public CustomerDeletedEventConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task Consume(ConsumeContext<CustomerDeletedIntegrationEvent> context)
        {
            var eventMessage = context.Message;
            return _emailSender.SendAsync(eventMessage.Email, 
                $"User {eventMessage.Email} has been deleted!", 
                $"Hello {eventMessage.Name}, your user has been deleted from the system.",
                CancellationToken.None);
        }
    }
}
