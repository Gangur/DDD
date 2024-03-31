using Domain.Abstraction;

namespace Domain.Products
{
    public interface IProductRepository : IRepository<Product, ProductId>
    {
        
    }
}
