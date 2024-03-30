using Domain.Abstraction;
using Domain.Orders;

namespace Domain.Customers
{
    public class Customer : IEntity<CustomerId>
    {
        public CustomerId Id { get; private set; }

        public string Email { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;
    }
}
