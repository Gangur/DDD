using Domain.Abstraction;
using Domain.Orders;

namespace Domain.LineItems.Transport
{
    public sealed record LineItemAddedDomailEvent(LineItemId Id, OrderId OrderId) : IDomainEvent<LineItemId>;
}
