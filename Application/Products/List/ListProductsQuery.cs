using Application.Abstraction;
using Domain.Products;

namespace Application.Products.List
{
    public record ListProductsQuery : IQuery<IReadOnlyCollection<Product>>;
}
