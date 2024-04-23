using Domain.Abstraction;
using Domain.Data;

namespace Domain.Products
{
    public class Product : BaseEntity<ProductId>
    {
        public Product() { }
        private Product(string name, Brand brand, string pictureName, Money price, Sku sku, Category category)
        {
            Id = new ProductId(Guid.NewGuid());
            Name = name;
            Brand = brand;
            Price = price;
            Sku = sku;
            PictureName = pictureName;
            Category = category;
        }

        public string Name { get; init; } = string.Empty;

        public Brand Brand { get; init; }

        public Category Category { get; init; }

        public string CategoryName { get => Category switch
        {
            Category.Phones => nameof(Category.Phones),
            Category.Tablets => nameof(Category.Tablets),
            Category.Books => nameof(Category.Books),
            _ => throw new NotImplementedException()
        }; }

        public string PictureName { get; init; } = string.Empty;

        public Money Price { get; init; } 

        public Sku Sku { get; init; }

        public static Product Create(string name, Brand brand, string pictureName, Money price, Sku sku, Category category)
        {
            var product = new Product(name, brand, pictureName, price, sku, category);

            product.Raise(new ProductCreatedDomainEvent(product.Id));

            return product;
        }
    }
}
