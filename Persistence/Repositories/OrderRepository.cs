using Domain.Customers;
using Domain.LineItems;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
            => await _context.FindAsync<Order>(entityId, cancellationToken);

        public async Task<Order?> FindWithIncludedLineItemAsync(OrderId orderId, LineItemId listItemId, CancellationToken cancellationToken)
            => await _context.GetAll<Order>()
                .Include(o => o.LineItems.Where(li => li.Id == listItemId))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<bool> HasOneLineItemAsync(OrderId orderId, CancellationToken cancellationToken)
        {
            var amount = await _context.GetQuery<LineItem>().CountAsync(o => o.OrderId == orderId, cancellationToken);

            return amount == 1;
        }

        public async Task<ICollection<Order>> ListAsync(CancellationToken cancellationToken)
            => await _context.GetQuery<Order>().ToListAsync(cancellationToken);

        public void Remove(Order entity)
            => _context.Remove(entity);
    }
}
