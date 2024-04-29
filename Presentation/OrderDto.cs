using Domain.Orders;

namespace Presentation
{
    public record OrderDto
    {
        public OrderDto() { }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<LineItemDto> LineItems { get; set; }

        public static OrderDto Map(Order order)
        {
            return new OrderDto()
            {
                Id = order.Id.Value,
                CustomerId = order.CustomerId.Value,
                LineItems = order.LineItems.Select(LineItemDto.Map).ToList(),
            };
        }
    }
}
