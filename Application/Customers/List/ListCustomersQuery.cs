using Application.Abstraction;
using Presentation;

namespace Application.Customers.List
{
    public record ListCustomersQuery : IQuery<IReadOnlyCollection<CustomerDto>>;
}
