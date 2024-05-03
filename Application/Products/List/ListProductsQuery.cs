using Application.Abstraction;
using Domain.Abstraction.Transport;
using Presentation;

namespace Application.Products.List
{
    public record ListProductsQuery(ListParameters ListParameters) : IQuery<ListResultDto<ProductDto>>;
}
