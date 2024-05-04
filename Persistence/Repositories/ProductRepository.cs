using Domain.Abstraction.Transport;
using Domain.Data;
using Domain.Products;
using Domain.Products.Transport;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

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

        public Task<int> CountAsync(ProductsListParameters parameters, CancellationToken cancellationToken)
            => ApplyFilters(_context.GetQuery<Product>(), parameters).CountAsync(cancellationToken);

        public async Task<Product?> FindAsync(ProductId entityId, CancellationToken cancellationToken)
            => await _context.FindAsync<Product>(entityId, cancellationToken);

        public Task<List<Product>> ListAsync(ProductsListParameters parameters, CancellationToken cancellationToken)
            => ApplyOrdering(ApplyFilters(_context.GetQuery<Product>(), parameters), parameters)
                .ToListAsync(cancellationToken);

        public IQueryable<Product> ApplyOrdering(IQueryable<Product> query, ListParameters parameters)
            => parameters.OrderBy switch
            {
                ProductOrderingTypes.Price =>   query.ApplyBaseListParameters(parameters, p => p.Price.Amount),
                ProductOrderingTypes.Name =>    query.ApplyBaseListParameters(parameters, p => p.Name),
                _ =>                            query.ApplyBaseListParameters(parameters, p => p.Id)
            };

        public void Remove(Product entity)
            => _context.Remove(entity);

        public Task<Product?> TakeAsync(ProductId entityId, CancellationToken cancellationToken)
            => _context.GetQuery<Product>()
                    .FirstOrDefaultAsync(p => p.Id == entityId, cancellationToken);

        public async Task<IReadOnlyCollection<string>> ListBrandsAsync(CancellationToken cancellationToken)
            => await _context.GetQuery<Product>().GroupBy(p => p.Brand).Select(p => p.Key.Name).ToListAsync(cancellationToken);

        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductsListParameters parameters)
        {
            if (parameters.Category != default)                     query = query.Where(p => p.Category == parameters.Category);
            if (!string.IsNullOrEmpty(parameters.Brand))            query = query.Where(p => p.Brand == Brand.Create(parameters.Brand));
            if (!string.IsNullOrEmpty(parameters.SearchString))     query = query.Where(p => EF.Functions.Like(p.Name, $"%{parameters.SearchString}%"));

            return query;
        }
    }
}
