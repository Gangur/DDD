using Domain.Abstraction;
using Domain.Data;
using Domain.Orders;
using Domain.Products;
using Remotion.Linq.Clauses;
using System.Diagnostics;

namespace Domain.LineItems
{
    public class LineItem : BaseEntity<LineItemId>
    {
        private LineItem() { }

        public int Quantity { get; private set; }

        public OrderId OrderId { get; private set; }

        public ProductId ProductId { get; private set; }
        public Product Product { get; private set; }

        public Money Price { get; private set; }

        public void Increment()
        {
            Quantity++;
        }

        public void Decrement(int quantity = 1)
        {
            if (Quantity > 0)
                Quantity -= quantity;
        }

        public static LineItem Create(LineItemId id, OrderId orderId, Product product)
        {
            return new LineItem()
            {
                Id = id,
                ProductId = product.Id,
                Product = product,
                Price = product.Price,
                OrderId = orderId,
                Quantity = 1
            };
        }
    }
}
