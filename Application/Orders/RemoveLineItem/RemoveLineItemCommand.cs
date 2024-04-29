using Application.Abstraction;
using Domain.LineItems;
using Domain.Orders;
using Domain.Products;
using Presentation;

namespace Application.Orders.RemoveLineItem
{
    public record RemoveLineItemCommand(OrderId OrderId, ProductId LineItemId, int Quantity) : ICommand<OrderDto>;
}
