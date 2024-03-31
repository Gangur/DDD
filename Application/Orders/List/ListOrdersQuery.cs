using Application.Abstraction;
using Domain.Orders;

namespace Application.Orders.List
{
    public record ListOrdersQuery : IQuery<IReadOnlyCollection<Order>>;
}
