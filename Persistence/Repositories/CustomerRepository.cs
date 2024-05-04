using Domain.Abstraction.Transport;
using Domain.Customers;
using Domain.Customers.Transport;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity, CancellationToken cancellationToken)
            => await _context.AddAsync(entity, cancellationToken);

        public Task<int> CountAsync(ListParameters parameters, CancellationToken cancellationToken)
            => _context.GetQuery<Customer>().CountAsync(cancellationToken);

        public async Task<Customer?> FindAsync(CustomerId entityId, CancellationToken cancellationToken)
            => await _context.FindAsync<Customer>(entityId, cancellationToken);

        public Task<List<Customer>> ListAsync(ListParameters parameters, CancellationToken cancellationToken)
            => ApplyOrdering(_context.GetQuery<Customer>(), parameters)
                .ToListAsync(cancellationToken);

        public IQueryable<Customer> ApplyOrdering(IQueryable<Customer> query, ListParameters parameters)
            => parameters.OrderBy switch
            {
                CustomerOrderingTypes.Name => query.ApplyBaseListParameters(parameters, p => p.Name!),
                CustomerOrderingTypes.Email => query.ApplyBaseListParameters(parameters, p => p.Email!),
                _ => query.ApplyBaseListParameters(parameters, p => p.Id)
            };

        public void Remove(Customer entity)
            => _context.Remove(entity);

        public Task<Customer?> TakeAsync(CustomerId entityId, CancellationToken cancellationToken)
            => _context.GetQuery<Customer>()
                        .FirstOrDefaultAsync(c => c.Id == entityId, cancellationToken);
    }
}
