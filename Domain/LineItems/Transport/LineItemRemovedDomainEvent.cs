using Domain.Abstraction;
using Domain.Orders;

namespace Domain.LineItems.Transport
{
    public sealed record LineItemRemovedDomainEvent(LineItemId Id, OrderId OrderId) : IDomainEvent<LineItemId>;
}
