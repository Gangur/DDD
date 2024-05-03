using Application.Abstraction;

namespace Application.Products.GetBranda
{
    public record ListBrandsQuery() : IQuery<IReadOnlyCollection<string>>;
}
