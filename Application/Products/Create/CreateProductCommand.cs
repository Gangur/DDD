using Application.Abstraction;
using Domain.Data;

namespace Application.Products.Create
{
    public record CreateProductCommand(string Name, Brand Brand, string PictureName, Money Price, Sku Sku, Category Category) : IDatabaseCommand<Guid>;
}
