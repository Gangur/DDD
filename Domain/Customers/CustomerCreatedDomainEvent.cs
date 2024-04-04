using Domain.Abstraction;

namespace Domain.Customers
{
    public record CustomerCreatedDomainEvent(CustomerId Id) : IDomainEvent<CustomerId>;
}
