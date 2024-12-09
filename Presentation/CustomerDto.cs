using Domain.Customers;

namespace Presentation
{
    public record CustomerDto
    {
        public CustomerDto() { }
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static CustomerDto Map(Customer customer)
        {
            return new CustomerDto()
            {
                Id = customer.Id.Value,
                Email = customer.Email ?? string.Empty,
                Name = customer.Name ?? string.Empty
            };
        }
    }
}
