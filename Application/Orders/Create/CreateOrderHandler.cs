using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Domain.Orders;

namespace Application.Orders.Create
{
    internal sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindAsync(request.customerId, cancellationToken);

            if (customer is null)
            {
                return Result.CreateFailed("The customer has not been found!");
            }

            var order = Order.Create(customer);

            await _orderRepository.AddAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
