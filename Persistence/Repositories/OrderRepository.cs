using Domain.Abstraction.Transport;
using Domain.Customers;
using Domain.Orders;
using Domain.Orders.Transport;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

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

        public Task<Order?> TakeByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
                .Where(o => o.Paid == default && o.AppUserId == userId)
                .Include(o => o.LineItems)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(cancellationToken);

        public void Remove(Order entity)
            => _context.Remove(entity);

        public Task<Order?> TakeAsync(OrderId entityId, CancellationToken cancellationToken)
            => _context.GetQuery<Order>()
                .Include(o => o.LineItems)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == entityId, cancellationToken);

        public Task<int> CountAsync(ListParameters parameters, CancellationToken cancellationToken)
            => _context.GetQuery<Order>().CountAsync(cancellationToken);

        public Task<Order[]> ListAsync(ListParameters parameters, CancellationToken cancellationToken)
            => ApplyOrdering(_context.GetQuery<Order>()
            .Include(o => o.LineItems)
                .ThenInclude(li => li.Product), parameters)
            .ToArrayAsync(cancellationToken);

        public IQueryable<Order> ApplyOrdering(IQueryable<Order> query, ListParameters parameters)
            => parameters.OrderBy switch
            {
                OrderOrderingTypes.Completed =>     query.ApplyBaseListParameters(parameters, p => p.Completed),
                _ =>                                query.ApplyBaseListParameters(parameters, p => p.Id)
            };
    }
}
