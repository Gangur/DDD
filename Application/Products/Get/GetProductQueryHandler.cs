using Application.Abstraction;
using Application.Data;
using Domain.Products;

namespace Application.Products.Get
{
    internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                return Result<Product>.CreateFailed("The product has not been found!");
            }

            return Result<Product>.CreateSuccessful(product);
        }
    }
}
