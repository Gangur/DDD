using Domain.Abstraction;

namespace Domain.Customers.Transport
{
    public record CustomerCreatedDomainEvent(CustomerId Id) : IDomainEvent<CustomerId>;
}
