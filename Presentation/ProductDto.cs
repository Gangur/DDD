namespace Presentation
{
    public record ProductDto(Guid Id, string Name, string PictureName, string PriceCurrency, decimal PriceAmount, string Sku);
}
