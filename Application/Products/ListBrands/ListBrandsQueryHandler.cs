using Application.Abstraction;
using Application.Data;
using Domain.Products;

namespace Application.Products.ListBrands
{
    public class ListBrandsQueryHandler : IQueryHandler<ListBrandsQuery, string[]>
    {
        private readonly IProductRepository _productRepository;

        public ListBrandsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<string[]>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
            => Result<string[]>.CreateSuccessful(await _productRepository.ListBrandsAsync(cancellationToken));
    }
}
