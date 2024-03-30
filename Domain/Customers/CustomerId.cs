using Domain.Abstraction;

namespace Domain.Customers
{
    public record CustomerId(Guid Value) : IEntityId;
}
