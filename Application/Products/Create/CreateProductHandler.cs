using Application.Abstraction;
using Application.Data;
using Domain.Data;
using Domain.Products;
using IntegrationEvents;

namespace Application.Products.Create
{
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventBus _eventBus;
        private readonly IBlobService _blobService;

        public CreateProductHandler(IProductRepository productRepository, IEventBus eventBus, IBlobService blobService)
        {
            _productRepository = productRepository;
            _eventBus = eventBus;
            _blobService = blobService;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (!await _blobService.ExistsAsync(request.PictureName, cancellationToken))
            {
                Result<Guid>.CreateValidationProblem($"The picture {request.PictureName} has not been found!");
            }

            var product = Product.Create(request.Name,
                request.Brand, 
                request.PictureName, 
                request.Price, 
                request.Sku, 
                request.Category);

            await _productRepository.AddAsync(product, cancellationToken);

            await _eventBus.PublishForAzureFunctionAsync(
                new ProductCreatedIntegrationEvent(product.Id.Value, product.Name, product.Price.Amount),
                cancellationToken);

            return Result<Guid>.CreateSuccessful(product.Id.Value);
        }
    }
}
