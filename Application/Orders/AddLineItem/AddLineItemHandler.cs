using Application.Abstraction;
using Application.Data;
using Domain.Data;
using Domain.Orders;
using Domain.Products;
using Presentation;

namespace Application.Orders.AddLineItem
{
    internal sealed class AddLineItemHandler : ICommandHandler<AddLineItemCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public AddLineItemHandler(IOrderRepository orderRepository,IProductRepository productRepository) 
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<OrderDto>> Handle(AddLineItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Result<OrderDto>.CreateValidationProblem("The order has not been found!");
            }

            var product = await _productRepository.FindAsync(request.ProductId, cancellationToken);

            if (product is null)
            {
                return Result<OrderDto>.CreateValidationProblem("The product has not been found!");
            }

            order.AddLineItem(product, request.Quantity);

            return Result<OrderDto>.CreateSuccessful(OrderDto.Map(order));
        }
    }
}
