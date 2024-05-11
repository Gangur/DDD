using Application.Abstraction;

namespace Application.Products.ListBrands
{
    public record ListBrandsQuery() : IQuery<string[]>;
}
