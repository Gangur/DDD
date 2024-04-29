using Domain.Customers;

namespace Presentation
{
    public record CustomerDto
    {
        public CustomerDto() { }
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }

        public static CustomerDto Map(Customer customer)
        {
            return new CustomerDto()
            {
                Id = customer.Id.Value,
                Email = customer.Email,
                Name = customer.Name
            };
        }
    }
}
