using Application.Abstraction;
using Application.Data;
using Domain.Data;
using Domain.Orders;
using Domain.Products;

namespace Application.Orders.AddLineItem
{
    internal sealed class AddLineItemHandler : ICommandHandler<AddLineItemCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public AddLineItemHandler(IOrderRepository orderRepository,IProductRepository productRepository) 
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(AddLineItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindWithIncludedLineItemAsync(request.OrderId, request.ProductId, cancellationToken);

            if (order is null)
            {
                return Result.CreateValidationProblem("The order has not been found!");
            }

            var lineItem = order.GetLineItemByProductId(request.ProductId);

            if (lineItem == default)
            {
                var product = await _productRepository.FindAsync(request.ProductId, cancellationToken);

                if (product is null)
                {
                    return Result.CreateValidationProblem("The product has not been found!");
                }

                order.AddLineItem(product);
            }
            else
            {
                lineItem.Increment();
            }

            return Result.CreateSuccessful();
        }
    }
}
