using Application.Abstraction;
using Domain.Customers;

namespace Application.Customers.List
{
    public record ListCustomersQuery : IQuery<IReadOnlyCollection<Customer>>;
}
