using Domain.Customers;
using Domain.LineItems;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order entity, CancellationToken cancellationToken)
            => await _context.AddAsync(entity, cancellationToken);

        public ValueTask<Order?> FindAsync(OrderId entityId, CancellationToken cancellationToken)
            => _context.FindAsync<Order>(entityId, cancellationToken);

        public Task<Order?> TakeByCustomerWithLineItemsAsync(CustomerId customerId, CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
                .Where(o => o.Paid == default && o.CustomerId == customerId)
                .Include(o => o.LineItems)
                .FirstOrDefaultAsync(cancellationToken);

        public Task<Order?> FindWithIncludedLineItemAsync(OrderId orderId, LineItemId listItemId, CancellationToken cancellationToken)
            => _context.GetSet<Order>()
                .Include(o => o.LineItems.Where(li => li.Id == listItemId))
                .FirstOrDefaultAsync(cancellationToken);

        public Task<Order?> FindWithIncludedLineItemAsync(OrderId orderId, ProductId productId, CancellationToken cancellationToken)
            => _context.GetSet<Order>()
                .Include(o => o.LineItems.Where(li => li.ProductId == productId))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<bool> HasOneLineItemAsync(OrderId orderId, CancellationToken cancellationToken)
        {
            var amount = await _context.GetQuery<LineItem>().CountAsync(o => o.OrderId == orderId, cancellationToken);

            return amount == 1;
        }

        public Task<List<Order>> ListAsync(CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
            .Include(o => o.LineItems)
            .ToListAsync(cancellationToken);

        public void Remove(Order entity)
            => _context.Remove(entity);

        public Task<Order?> TakeAsync(OrderId entityId, CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
                .Include(o => o.LineItems)
                .FirstOrDefaultAsync(o => o.Id == entityId, cancellationToken);
    }
}
