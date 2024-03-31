using Application.Abstraction;
using Application.Data;
using Domain.Products;

namespace Application.Products.List
{
    internal sealed class ListProductsQueryHandler : IQueryHandler<ListProductsQuery, IReadOnlyCollection<Product>>
    {
        private readonly IProductRepository _productRepository;

        public ListProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IReadOnlyCollection<Product>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.ListAsync(cancellationToken);

            return Result<IReadOnlyCollection<Product>>.CreateSuccessful(products);
        }
    }
}
