using Application.Abstraction;
using Domain.Orders;

namespace Application.Orders.Get
{
    public record GetOrderQuery(OrderId OrderId) : IQuery<Order>;
}
