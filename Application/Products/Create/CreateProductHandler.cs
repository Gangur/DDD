using Application.Abstraction;
using Application.Data;
using Domain.Products;
using IntegrationEvents;

namespace Application.Products.Create
{
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventBus _eventBus;

        public CreateProductHandler(IProductRepository productRepository, IEventBus eventBus)
        {
            _productRepository = productRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Price, request.Sku);

            await _productRepository.AddAsync(product, cancellationToken);

            await _eventBus.PublishForAzureFunctionAsync(
                new ProductCreatedIntegrationEvent(product.Id.Value, product.Name, product.Price.Amount),
                cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
