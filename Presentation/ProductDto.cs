using Domain.Products;

namespace Presentation
{
    public record ProductDto
    {
        private ProductDto() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string PictureName { get; set; }
        public string PriceCurrency { get; set; }
        public decimal PriceAmount { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }

        public static ProductDto Map(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id.Value,
                Name = product.Name,
                Brand = product.Brand.Name,
                PictureName = product.PictureName,
                PriceCurrency = product.Price.Currency,
                PriceAmount = product.Price.Amount,
                Sku = product.Sku.Value,
                Category = product.CategoryName
            };
        }
    }
}
