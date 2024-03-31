using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Domain.Products;
using Persistence;

namespace Application.Products.Get
{
    internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, Product>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetProductQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.FindAsync<Product>(request.ProductId, cancellationToken);

            if (product == null)
            {
                return Result<Product>.CreateFailed("The product has not been found!");
            }

            return Result<Product>.CreateSuccessful(product);
        }
    }
}
