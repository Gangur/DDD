using Domain.Abstraction;
using Domain.Data;

namespace Domain.Products
{
    public class Product : Entity<ProductId>
    {
        private Product(string name, Money price, Sku sku)
        {
            Id = new ProductId(Guid.NewGuid());
            Name = name;
            Price = price;
            Sku = sku;
        }

        public ProductId Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public Money Price { get; private set; } 

        public Sku Sku { get; private set; }

        public static Product Create(string name, Money price, Sku sku)
        {
            var product = new Product(name, price, sku);

            product.Raise(new ProductCreatedDomainEvent(product.Id));

            return product;
        }
    }
}
