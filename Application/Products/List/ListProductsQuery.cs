using Application.Abstraction;
using Domain.Products.Transport;
using Presentation;
using Presentation.Adstraction;

namespace Application.Products.List
{
    public record ListProductsQuery(ProductsListParameters ListParameters) : IQuery<ListResultDto<ProductDto>>;
}
