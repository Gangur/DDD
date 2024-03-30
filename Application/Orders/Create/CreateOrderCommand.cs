using Application.Abstraction;
using Domain.Customers;

namespace Application.Orders.Create
{
    public record CreateOrderCommand(CustomerId customerId) : ICommand;
}
