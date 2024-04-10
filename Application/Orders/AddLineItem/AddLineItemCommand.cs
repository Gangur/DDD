using Application.Abstraction;
using Domain.Orders;
using Domain.Products;

namespace Application.Orders.AddLineItem
{
    public record AddLineItemCommand(OrderId OrderId, ProductId ProductId) : IDatabaseCommand;
}
