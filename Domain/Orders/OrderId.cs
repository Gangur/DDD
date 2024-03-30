using Domain.Abstraction;

namespace Domain.Orders
{
    public record OrderId(Guid Value) : IEntityId;
}
