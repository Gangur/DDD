using Application.Abstraction;
using Domain.Data;

namespace Application.Products.Create
{
    public record CreateProductCommand(string Name, Money Price, Sku Sku) : ICommand;
}
