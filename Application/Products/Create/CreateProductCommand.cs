using Application.Abstraction;
using Domain.Data;
using Domain.Products;

namespace Application.Products.Create
{
    public record CreateProductCommand(string Name, Brand Brand, string PictureName, Money Price, Sku Sku, Category Category) : ICommand<ProductId>;
}
