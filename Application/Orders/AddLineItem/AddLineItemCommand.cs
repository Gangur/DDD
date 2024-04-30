using Application.Abstraction;
using Domain.Orders;
using Domain.Products;
using Presentation;

namespace Application.Orders.AddLineItem
{
    public record AddLineItemCommand(OrderId OrderId, ProductId ProductId, int Quantity) : ICommand<OrderDto>;
}
