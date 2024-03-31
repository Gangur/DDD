using Application.Abstraction;
using Application.Data;
using Domain.Products;
using Persistence;

namespace Application.Products.Create
{
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateProductHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Price, request.Sku);

            await _dbContext.GetAll<Product>().AddAsync(product, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
