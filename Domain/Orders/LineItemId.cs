using Domain.Abstraction;

namespace Domain.Orders
{
    public record LineItemId(Guid Value) : IEntityId;
}
