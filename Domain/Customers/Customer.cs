using Domain.Abstraction;

namespace Domain.Customers
{
    public class Customer : BaseEntity<CustomerId>
    {
        private Customer()
        {
            Id = new CustomerId(Guid.NewGuid());
        }

        public string? Email { get; private set; }

        public string? Name { get; private set; }


        public static Customer Create()
        {
            var customer = new Customer();

            customer.Raise(new CustomerCreatedDomainEvent(customer.Id));

            return customer;
        }
    }
}
