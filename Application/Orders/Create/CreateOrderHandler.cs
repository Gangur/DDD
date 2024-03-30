using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;
using Persistence;

namespace Application.Orders.Create
{
    internal sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateOrderHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.GetAll<Customer>()
                .FindAsync(request.customerId, cancellationToken);
            
            if (customer is null)
            {
                return Result.CreateFailed("Клиент не найден!");
            }

            var order = Order.Create(customer);

            await _dbContext.GetAll<Order>().AddAsync(order, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
