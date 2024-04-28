using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Presentation;

namespace Application.Orders.Get
{
    internal sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            Order? order = default;
            if (request.OrderId != default)
            {
                order = await _orderRepository.TakeAsync(request.OrderId!, cancellationToken);
            }
            else if (request.CustomerId != default)
            {
                order = await _orderRepository
                    .TakeByCustomerWithLineItemsAsync(request.CustomerId!, cancellationToken);
            }

            if (order == null)
            {
                return Result<OrderDto>.CreateNotFount("The order has not been found!");
            }

            return Result<OrderDto>.CreateSuccessful(new OrderDto(
                order.Id.Value, 
                order.CustomerId.Value,
                order.LineItems.Select(li => new LineItemDto(li.ProductId.Value, li.Quantity)).ToList()));
        }
    }
}
