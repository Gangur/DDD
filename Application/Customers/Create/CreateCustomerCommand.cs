using Application.Abstraction;
using Domain.Customers;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand() : ICommand<CustomerId>;
}
