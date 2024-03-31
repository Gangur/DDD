using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product entity, CancellationToken cancellationToken)
            => await _context.AddAsync(entity, cancellationToken);

        public async Task<Product?> FindAsync(ProductId entityId, CancellationToken cancellationToken)
            => await _context.FindAsync<Product>(entityId, cancellationToken);

        public async Task<IReadOnlyCollection<Product>> ListAsync(CancellationToken cancellationToken)
            => await _context.GetQuery<Product>().ToListAsync(cancellationToken);
    }
}
