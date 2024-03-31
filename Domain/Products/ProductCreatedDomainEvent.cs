using Domain.Abstraction;

namespace Domain.Products
{
    public sealed record ProductCreatedDomainEvent(ProductId Id) : IDomainEvent<ProductId>;
}
