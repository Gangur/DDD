using Application.Abstraction;
using Application.Auth;
using Application.Data;
using Domain.Orders;
using Presentation;

namespace Application.Orders.Get
{
    internal sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly AuthService _authService;

        public GetOrderQueryHandler(
            IOrderRepository orderRepository, 
            AuthService authService)
        {
            _orderRepository = orderRepository;
            _authService = authService;
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
            else
            {
                order = await _orderRepository.TakeByUserIdAsync(_authService.UserId, cancellationToken);
            }

            if (order == null)
            {
                return Result<OrderDto>.CreateNotFount("The order has not been found!");
            }

            return Result<OrderDto>.CreateSuccessful(OrderDto.Map(order));
        }
    }
}
