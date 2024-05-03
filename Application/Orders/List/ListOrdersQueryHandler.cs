using Application.Abstraction;
using Application.Data;
using Domain.Abstraction.Transport;
using Domain.Orders;
using Presentation;

namespace Application.Orders.List
{
    internal sealed class ListOrdersQueryHandler : IQueryHandler<ListOrdersQuery, ListResultDto<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<ListResultDto<OrderDto>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            var ordersTotal = await _orderRepository.CountAsync(request.ListParameters, cancellationToken);
            var orders = await _orderRepository.ListAsync(request.ListParameters, cancellationToken);

            var listResult = ListResultDto<OrderDto>
                .Create(ordersTotal, orders.Select(OrderDto.Map).ToList());

            return Result<ListResultDto<OrderDto>>
                .CreateSuccessful(listResult);
        }
    }
}
