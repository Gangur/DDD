namespace Presentation
{
    public record ProductDto(Guid Id, 
        string Name, 
        string Brand,
        string PictureName, 
        string PriceCurrency, 
        decimal PriceAmount, 
        string Sku,
        string Category);
}
