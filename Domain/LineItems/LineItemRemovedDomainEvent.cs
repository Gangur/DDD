using Domain.Abstraction;
using Domain.Orders;

namespace Domain.LineItems
{
    public sealed record LineItemRemovedDomainEvent(LineItemId Id, OrderId OrderId) : IDomainEvent<LineItemId>;
}
