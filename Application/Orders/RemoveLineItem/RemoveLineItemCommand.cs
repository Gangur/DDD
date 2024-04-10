using Application.Abstraction;
using Domain.LineItems;
using Domain.Orders;
using MediatR;

namespace Application.Orders.RemoveLineItem
{
    public record RemoveLineItemCommand(OrderId OrderId, LineItemId LineItemId) : IDatabaseCommand;
}
