using Domain.Abstraction;
using Domain.Products.Transport;

namespace Domain.Products
{
    public interface IProductRepository : IRepository<Product, ProductId, ProductsListParameters>
    {
        Task<string[]> ListBrandsAsync(CancellationToken cancellationToken);
    }
}
