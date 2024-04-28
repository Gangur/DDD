using Application.Abstraction;
using Domain.Customers;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand(string Email, string Name) : IDatabaseCommand<CustomerId>;
}
