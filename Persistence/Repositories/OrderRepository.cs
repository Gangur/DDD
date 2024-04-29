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

        public async Task<Order?> FindAsync(OrderId entityId, CancellationToken cancellationToken)
            => await _context.GetSet<Order>()
                .Include(o => o.LineItems)
                    .ThenInclude(li => li.Product)
                .FirstOrDefaultAsync(o => o.Id == entityId, cancellationToken);

        public Task<Order?> TakeByCustomerWithLineItemsAsync(CustomerId customerId, CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
                .Where(o => o.Paid == default && o.CustomerId == customerId)
                .Include(o => o.LineItems)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(cancellationToken);

        public Task<List<Order>> ListAsync(CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
            .Include(o => o.LineItems)
                .ThenInclude(li => li.Product)
            .ToListAsync(cancellationToken);

        public void Remove(Order entity)
            => _context.Remove(entity);

        public Task<Order?> TakeAsync(OrderId entityId, CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
                .Include(o => o.LineItems)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == entityId, cancellationToken);
    }
}
