using Application.Abstraction;
using Presentation;

namespace Application.Products.List
{
    public record ListProductsQuery : IQuery<IReadOnlyCollection<ProductDto>>;
}
