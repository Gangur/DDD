using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.List
{
    internal sealed class ListOrdersQueryHandler : IQueryHandler<ListOrdersQuery, IReadOnlyCollection<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<IReadOnlyCollection<Order>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            var products = await _orderRepository.ListAsync(cancellationToken);

            return Result<IReadOnlyCollection<Order>>.CreateSuccessful(products);
        }
    }
}
