using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Persistence;

namespace Application.Customers.Create
{
    internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateCustomerHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create(request.Email, request.Name);

            await _dbContext.GetAll<Customer>().AddAsync(customer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
