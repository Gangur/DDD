using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Customers.List
{
    internal sealed class ListCustomersQueryHandler : IQueryHandler<ListCustomersQuery, IReadOnlyCollection<Customer>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListCustomersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IReadOnlyCollection<Customer>>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.GetQuery<Customer>().ToListAsync(cancellationToken);

            return Result<IReadOnlyCollection<Customer>>.CreateSuccessful(products.AsReadOnly());
        }
    }
}
