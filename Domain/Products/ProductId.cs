using Domain.Abstraction;

namespace Domain.Products
{
    public record ProductId(Guid Value) : IEntityId;
}
