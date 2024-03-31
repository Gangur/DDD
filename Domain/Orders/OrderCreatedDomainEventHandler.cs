using IntegrationEvemts;
using MediatR;
using Rebus.Bus;

namespace Domain.Orders
{
    internal sealed class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IBus _bus;
        private OrderCreatedDomainEventHandler(IBus bus) => _bus = bus;

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Send(new OrderCreatedIntegrationEvent(notification.Id.Value));
        }
    }
}
