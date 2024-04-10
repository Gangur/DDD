namespace Presentation
{
    public record ProductDto(Guid Id, string Name, string PriceCurrency, decimal PriceAmount, string Sku);
}
