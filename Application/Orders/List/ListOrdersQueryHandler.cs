using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.List
{
    internal sealed class ListOrdersQueryHandler : IQueryHandler<ListOrdersQuery, IReadOnlyCollection<Order>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListOrdersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IReadOnlyCollection<Order>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.GetQuery<Order>().ToListAsync(cancellationToken);

            return Result<IReadOnlyCollection<Order>>.CreateSuccessful(products.AsReadOnly());
        }
    }
}
