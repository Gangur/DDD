using Application.Abstraction;
using Domain.Customers;

namespace Application.Customers.Delete
{
    public sealed record DeleteCustomerCommand(CustomerId CustomerId) : ICommand;
}
