using Domain.Data;
using Domain.LineItems;

namespace Presentation
{
    public record LineItemDto
    {
        public LineItemDto() { }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureName { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }

        public static LineItemDto Map(LineItem lineItem)
        {
            return new LineItemDto()
            {
                ProductId = lineItem.ProductId.Value,
                ProductName = lineItem.Product.Name,
                PictureName = lineItem.Product.PictureName,
                PriceAmount = lineItem.Price.Amount,
                PriceCurrency = lineItem.Price.Currency,
                Category = lineItem.Product.Category,
                Quantity = lineItem.Quantity
            };
        }
    }
}
