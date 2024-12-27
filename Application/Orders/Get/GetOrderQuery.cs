using Application.Abstraction;
using Domain.Customers;
using Domain.Orders;
using Presentation;

namespace Application.Orders.Get
{
    public record GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery() { }

        public GetOrderQuery(OrderId orderId)
        {
            OrderId = orderId;
        }

        public GetOrderQuery(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public OrderId? OrderId { get; private set; }

        public CustomerId? CustomerId { get; private set; }
    }
}
