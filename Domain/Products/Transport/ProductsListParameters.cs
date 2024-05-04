using Domain.Abstraction.Transport;
using Domain.Data;

namespace Domain.Products.Transport
{
    public class ProductsListParameters : ListParameters
    {
        public Category? Category { get; set; }

        public string? Brand { get; set; }

        public string? SearchString { get; set; }
    }
}
