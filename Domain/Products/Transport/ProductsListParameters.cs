using Domain.Abstraction.Transport;
using Domain.Data;

namespace Domain.Products.Transport
{
    public class ProductsListParameters : ListParameters
    {
        public Category[] Categories { get; set; } = Array.Empty<Category>();

        public string[] Brands { get; set; } = Array.Empty<string>();

        public string SearchString { get; set; } = string.Empty;
    }
}
