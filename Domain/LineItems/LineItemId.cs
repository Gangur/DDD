using Domain.Abstraction;

namespace Domain.LineItems
{
    public record LineItemId(Guid Value) : IEntityId;
}
