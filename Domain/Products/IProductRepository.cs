using Domain.Abstraction;
using Domain.Abstraction.Transport;

namespace Domain.Products
{
    public interface IProductRepository : IRepository<Product, ProductId, ListParameters>
    {
        Task<IReadOnlyCollection<string>> ListBrandsAsync(CancellationToken cancellationToken);
    }
}
