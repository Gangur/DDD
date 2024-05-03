using Application.Abstraction;
using Domain.Abstraction.Transport;
using Domain.Orders;
using Presentation;

namespace Application.Orders.List
{
    public record ListOrdersQuery(ListParameters ListParameters) : IQuery<ListResultDto<OrderDto>>;
}
