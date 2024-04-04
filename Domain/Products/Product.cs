using Domain.Abstraction;
using Domain.Data;

namespace Domain.Products
{
    public class Product : BaseEntity<ProductId>
    {
        public Product() { }
        private Product(string name, Money price, Sku sku)
        {
            Id = new ProductId(Guid.NewGuid());
            Name = name;
            Price = price;
            Sku = sku;
        }

        public ProductId Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public Money Price { get; init; } 

        public Sku Sku { get; init; }

        public static Product Create(string name, Money price, Sku sku)
        {
            var product = new Product(name, price, sku);

            product.Raise(new ProductCreatedDomainEvent(product.Id));

            return product;
        }
    }
}
