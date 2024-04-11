using Application.Abstraction;
using Domain.Data;

namespace Application.Products.Create
{
    public record CreateProductCommand(string Name, string PictureName, Money Price, Sku Sku) : IDatabaseCommand<Guid>;
}
