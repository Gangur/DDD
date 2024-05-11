using Application.Abstraction;
using Application.Data;
using Domain.Products;
using Presentation;
using Presentation.Adstraction;

namespace Application.Products.List
{
    internal sealed class ListProductsQueryHandler : IQueryHandler<ListProductsQuery, ListResultDto<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public ListProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ListResultDto<ProductDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var productsTotal = await _productRepository.CountAsync(request.ListParameters, cancellationToken);
            var products = await _productRepository.ListAsync(request.ListParameters, cancellationToken);

            var listResult = ListResultDto<ProductDto>
                .Create(productsTotal, products.Select(ProductDto.Map).ToArray());

            return Result<ListResultDto<ProductDto>>.CreateSuccessful(listResult);
        }
    }
}
