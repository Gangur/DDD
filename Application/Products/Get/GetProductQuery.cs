using Application.Abstraction;
using Domain.Products;
using Presentation;

namespace Application.Products.Get
{
    public record GetProductQuery(ProductId ProductId) : IQuery<ProductDto>;
}
