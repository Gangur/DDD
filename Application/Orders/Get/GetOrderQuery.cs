using Application.Abstraction;
using Domain.Orders;
using Presentation;

namespace Application.Orders.Get
{
    public record GetOrderQuery(OrderId OrderId) : IQuery<OrderDto>;
}
