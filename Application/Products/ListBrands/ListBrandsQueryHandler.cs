using Application.Abstraction;
using Application.Data;
using Application.Products.GetBranda;
using Domain.Products;

namespace Application.Products.GetBrands
{
    public class ListBrandsQueryHandler : IQueryHandler<ListBrandsQuery, IReadOnlyCollection<string>>
    {
        private readonly IProductRepository _productRepository;

        public ListBrandsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IReadOnlyCollection<string>>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
            => Result<IReadOnlyCollection<string>>.CreateSuccessful(await _productRepository.ListBrandsAsync(cancellationToken));
    }
}
