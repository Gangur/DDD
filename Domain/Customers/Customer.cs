using Domain.Abstraction;

namespace Domain.Customers
{
    public class Customer : Entity<CustomerId>
    {
        public Customer() { }
        private Customer(string email, string name)
        {
            Id = new CustomerId(Guid.NewGuid());
            Email = email;
            Name = name;
        }

        public CustomerId Id { get; private set; }

        public string Email { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;


        public static Customer Create(string email, string name) 
            => new Customer(email, name);
    }
}
