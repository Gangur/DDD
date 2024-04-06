using Domain.Abstraction;

namespace Domain.Customers
{
    public class Customer : BaseEntity<CustomerId>
    {
        public Customer() { }
        private Customer(string email, string name)
        {
            Id = new CustomerId(Guid.NewGuid());
            Email = email;
            Name = name;
        }

        public string Email { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;


        public static Customer Create(string email, string name)
        {
            var customer = new Customer(email, name);

            customer.Raise(new CustomerCreatedDomainEvent(customer.Id));

            return customer;
        }
    }
}
