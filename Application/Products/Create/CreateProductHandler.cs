using Application.Abstraction;
using Application.Data;
using Domain.Products;

namespace Application.Products.Create
{
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Price, request.Sku);

            await _productRepository.AddAsync(product, cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
