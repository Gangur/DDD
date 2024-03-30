using Domain.Abstraction;

namespace Domain.Products
{
    public class Product : IEntity<ProductId>
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public Money Price { get; private set; } // immutable

        public Sku Sku { get; private set; }
    }
}
