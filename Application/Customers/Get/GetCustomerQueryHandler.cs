using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Persistence;

namespace Application.Customers.Get
{
    internal sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, Customer>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.FindAsync<Customer>(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return Result<Customer>.CreateFailed("The customer has not been found!");
            }

            return Result<Customer>.CreateSuccessful(customer);
        }
    }
}
