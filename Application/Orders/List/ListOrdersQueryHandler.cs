using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Domain.Orders;
using Presentation;
using Presentation.Adstraction;

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
                .Create(ordersTotal, Array.ConvertAll(orders,
                    new Converter<Order, OrderDto>(OrderDto.Map)));

            return Result<ListResultDto<OrderDto>>
                .CreateSuccessful(listResult);
        }
    }
}
