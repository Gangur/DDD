using Domain.Abstraction;
using Domain.LineItems;

namespace Domain.Orders
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        Task<Order?> FindWithIncludedLineItemAsync(OrderId orderId, LineItemId listItemId, CancellationToken cancellationToken);

        Task<bool> HasOneLineItemAsync(OrderId orderId, CancellationToken cancellationToken);
    }
}
