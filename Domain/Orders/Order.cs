using Domain.Abstraction;
using Domain.Customers;
using Domain.LineItems;
using Domain.Products;

namespace Domain.Orders
{
    public class Order : Entity<OrderId>
    {
        private readonly HashSet<LineItem> _lineItems = new();

        private Order() { }

        public OrderId Id { get; private set; }

        public CustomerId CustomerId { get; private set; }

        public HashSet<LineItem> LineItems { get => _lineItems; }

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

        public void AddLineItem(Product pruduct)
        {
            var lineItem = new LineItem(
                new LineItemId(Guid.NewGuid()), 
                Id, 
                pruduct.Id, 
                pruduct.Price);

            _lineItems.Add(lineItem);

            Raise(new LineItemAddedDomailEvent(lineItem.Id, Id));
        }

        public void RemoveLineItem(LineItemId lineItemId)
        {
            var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

            if (lineItem is null)
            {
                return;
            }

            _lineItems.Remove(lineItem);

            Raise(new LineItemRemovedDomainEvent(lineItem.Id, Id));
        }
    }
}
