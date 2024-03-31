using Application.Abstraction;
using Application.Data;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Products.List
{
    internal sealed class ListProductsQueryHandler : IQueryHandler<ListProductsQuery, IReadOnlyCollection<Product>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListProductsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IReadOnlyCollection<Product>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.GetQuery<Product>().ToListAsync(cancellationToken);

            return Result<IReadOnlyCollection<Product>>.CreateSuccessful(products.AsReadOnly());
        }
    }
}
