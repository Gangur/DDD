using Domain.Abstraction;
using Domain.Customers;
using Domain.LineItems;
using Domain.Products;

namespace Domain.Orders
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        Task<Order?> TakeByCustomerWithLineItemsAsync(CustomerId customerId, CancellationToken cancellationToken);
    }
}
