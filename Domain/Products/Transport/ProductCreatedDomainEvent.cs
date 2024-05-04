using Domain.Abstraction;

namespace Domain.Products.Transport
{
    public sealed record ProductCreatedDomainEvent(ProductId Id) : IDomainEvent<ProductId>;
}
