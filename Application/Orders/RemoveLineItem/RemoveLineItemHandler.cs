using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Presentation;

namespace Application.Orders.RemoveLineItem
{
    internal sealed class RemoveLineItemHandler : ICommandHandler<RemoveLineItemCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveLineItemHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<OrderDto>> Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Result<OrderDto>.CreateValidationProblem("The order has not been found!");
            }

            order.RemoveLineItem(request.LineItemId, request.Quantity);

            return Result<OrderDto>.CreateSuccessful(OrderDto.Map(order));
        }
    }
}
