using Application.Abstraction;
using Domain.Orders;
using Presentation;

namespace Application.Orders.List
{
    public record ListOrdersQuery : IQuery<IReadOnlyCollection<OrderDto>>;
}
