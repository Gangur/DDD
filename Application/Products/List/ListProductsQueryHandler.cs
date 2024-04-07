using Application.Abstraction;
using Application.Data;
using Domain.Products;
using Presentation;

namespace Application.Products.List
{
    internal sealed class ListProductsQueryHandler : IQueryHandler<ListProductsQuery, IReadOnlyCollection<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public ListProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IReadOnlyCollection<ProductDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.ListAsync(cancellationToken);

            return Result<IReadOnlyCollection<ProductDto>>
                .CreateSuccessful(products
                    .Select(p => new ProductDto(p.Id.Value, p.Name, p.Price.Currency, p.Price.Amount, p.Sku.Value))
                    .ToList());
        }
    }
}
