using Domain.Abstraction;
using Domain.Products.Transport;

namespace Domain.Products
{
    public interface IProductRepository : IRepository<Product, ProductId, ProductsListParameters>
    {
        Task<IReadOnlyCollection<string>> ListBrandsAsync(CancellationToken cancellationToken);
    }
}
