using Domain.Abstraction;
using Domain.Data;
using Domain.Orders;
using Domain.Products;

namespace Domain.LineItems
{
    public class LineItem : Entity<LineItemId>
    {
        internal LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price) : this(id, orderId)
        {
            ProductId = productId;
            Price = price;
        }

        private LineItem(LineItemId id, OrderId orderId)
        {
            Id = id;
            OrderId = orderId;
        }

        public LineItemId Id { get; private set; }

        public OrderId OrderId { get; private set; }

        public ProductId ProductId { get; private set; }

        public Money Price { get; private set; }
    }
}
