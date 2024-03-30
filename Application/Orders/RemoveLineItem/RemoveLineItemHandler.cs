using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.RemoveLineItem
{
    internal sealed class RemoveLineItemHandler : ICommandHandler<RemoveLineItemCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        public RemoveLineItemHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.GetAll<Order>()
                .Include(o => o.LineItems.Where(li => li.Id == request.LineItemId))
                //.AsSplitQuery()
                .SingleOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

            if (order is null)
            {
                return Result.CreateFailed("Заказ не найден!");
            }

            order.RemoveLineItem(request.LineItemId);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
