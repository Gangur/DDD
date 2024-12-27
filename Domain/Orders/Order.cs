using Domain.Abstraction;
using Domain.Customers;
using Domain.LineItems;
using Domain.LineItems.Transport;
using Domain.Orders.Transport;
using Domain.Products;
using Domain.User;

namespace Domain.Orders
{
    public class Order : BaseEntity<OrderId>
    {
        private readonly HashSet<LineItem> _lineItems = new();

        private Order() { }

        public CustomerId CustomerId { get; private set; }

        public DateTime? Paid { get; private set; }

        public DateTime? Completed { get; private set; }

        public Guid? AppUserId { get; private set; }

        public AppUser AppUser { get; private set; }

        public HashSet<LineItem> LineItems { get => _lineItems; }

        public void AssignUser(AppUser appUser)
        {
            AppUserId = appUser.Id;
        }

        public static Order Create(Customer customer)
        {
            var order = new Order()
            {
                Id = new OrderId(Guid.NewGuid()),
                CustomerId = customer.Id,
            };

            order.Raise(new OrderCreatedDomainEvent(order.Id));

            return order;
        }

        public void AddLineItem(Product product, int quantity = 1)
        {
            var lineItem = _lineItems.FirstOrDefault(li => li.ProductId == product.Id);

            if (lineItem == default)
            {
                lineItem = LineItem.Create(
                    new LineItemId(Guid.NewGuid()),
                    Id,
                    product,
                    quantity);

                _lineItems.Add(lineItem);

                Raise(new LineItemAddedDomailEvent(lineItem.Id, Id));
            }
            else
            {
                lineItem.Increment(quantity);
            }
        }

        public void RemoveLineItem(ProductId productId, int quantity = 1)
        {
            var lineItem = _lineItems.FirstOrDefault(li => li.ProductId == productId);

            if (lineItem == default)
            {
                return;
            }
            else
            {
                lineItem.Decrement(quantity);

                if (lineItem.Quantity <= 0)
                    _lineItems.Remove(lineItem);

                Raise(new LineItemRemovedDomainEvent(lineItem.Id, Id));
            }
        }
    }
}
