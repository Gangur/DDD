using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Domain.Orders;
using Persistence;

namespace Application.Orders.Get
{
    internal sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, Order>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetOrderQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.FindAsync<Order>(request.OrderId, cancellationToken);

            if (order == null)
            {
                return Result<Order>.CreateFailed("The order has not been found!");
            }

            return Result<Order>.CreateSuccessful(order);
        }
    }
}
