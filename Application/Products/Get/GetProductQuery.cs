using Application.Abstraction;
using Domain.Products;

namespace Application.Products.Get
{
    public record GetProductQuery(ProductId ProductId) : IQuery<Product>;
}
