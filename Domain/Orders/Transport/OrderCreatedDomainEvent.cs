using Domain.Abstraction;

namespace Domain.Orders.Transport
{
    public sealed record OrderCreatedDomainEvent(OrderId Id) : IDomainEvent<OrderId>;
}
