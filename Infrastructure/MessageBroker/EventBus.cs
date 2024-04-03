using Application.Abstraction;
using MassTransit;

namespace Infrastructure.MessageBroker
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageSender _messageSender;

        public EventBus(IPublishEndpoint publishEndpoint, IMessageSender messageSender)
        {
            _publishEndpoint = publishEndpoint;
            _messageSender = messageSender;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken)
            where T : class
            => _publishEndpoint.Publish(message, cancellationToken);

        public Task PublishForAzureFunctionAsync<T>(T message, CancellationToken cancellationToken)
            where T : class
            => _messageSender.SendAsync(message, cancellationToken);
    }
}
