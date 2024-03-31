using Domain.Customers;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        internal CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity, CancellationToken cancellationToken)
            => await _context.AddAsync(entity, cancellationToken);

        public async Task<Customer?> FindAsync(CustomerId entityId, CancellationToken cancellationToken)
            => await _context.FindAsync<Customer>(entityId, cancellationToken);

        public async Task<IReadOnlyCollection<Customer>> ListAsync(CancellationToken cancellationToken)
            => await _context.GetQuery<Customer>().ToListAsync(cancellationToken);
    }
}
