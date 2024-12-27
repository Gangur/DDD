using Domain.Abstraction;
using Domain.Abstraction.Transport;
using Domain.Customers;

namespace Domain.Orders
{
    public interface IOrderRepository : IRepository<Order, OrderId, ListParameters>
    {
        Task<Order?> TakeByCustomerWithLineItemsAsync(CustomerId customerId, CancellationToken cancellationToken);

        Task<Order?> TakeByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
