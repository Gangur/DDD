using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Presentation;

namespace Application.Orders.List
{
    internal sealed class ListOrdersQueryHandler : IQueryHandler<ListOrdersQuery, IReadOnlyCollection<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<IReadOnlyCollection<OrderDto>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.ListAsync(cancellationToken);

            return Result<IReadOnlyCollection<OrderDto>>
                .CreateSuccessful(orders.Select(OrderDto.Map).ToList());
        }
    }
}
