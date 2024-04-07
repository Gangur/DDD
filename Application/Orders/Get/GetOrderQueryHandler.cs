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
            var order = await _orderRepository.FindAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                return Result<OrderDto>.CreateFailed("The order has not been found!");
            }

            return Result<OrderDto>.CreateSuccessful(new OrderDto(order.Id.Value, order.CustomerId.Value));
        }
    }
}
