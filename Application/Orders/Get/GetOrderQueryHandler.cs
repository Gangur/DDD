using Application.Abstraction;
using Application.Data;
using Domain.Orders;

namespace Application.Orders.Get
{
    internal sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                return Result<Order>.CreateFailed("The order has not been found!");
            }

            return Result<Order>.CreateSuccessful(order);
        }
    }
}
