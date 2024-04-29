using Application.Abstraction;
using Domain.Customers;
using Domain.Orders;

namespace Application.Orders.Create
{
    public record CreateOrderCommand(CustomerId customerId) : ICommand<OrderId>;
}
