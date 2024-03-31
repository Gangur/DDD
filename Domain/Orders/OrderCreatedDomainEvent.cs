using Domain.Abstraction;

namespace Domain.Orders
{
    public sealed record OrderCreatedDomainEvent(OrderId Id) : IDomainEvent<OrderId>;
}
