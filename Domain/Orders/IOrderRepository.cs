using Domain.Abstraction;
using Domain.Customers;
using Domain.LineItems;
using Domain.Products;

namespace Domain.Orders
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        Task<Order?> FindWithIncludedLineItemAsync(OrderId orderId, LineItemId listItemId, CancellationToken cancellationToken);

        Task<Order?> FindWithIncludedLineItemAsync(OrderId orderId, ProductId productId, CancellationToken cancellationToken);

        Task<Order?> TakeByCustomerWithLineItemsAsync(CustomerId customerId, CancellationToken cancellationToken);

        Task<bool> HasOneLineItemAsync(OrderId orderId, CancellationToken cancellationToken);
    }
}
