using Application.Abstraction;
using Application.Data;
using Domain.Customers;
namespace Application.Customers.Create
{
    internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, CustomerId>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<CustomerId>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create();

            await _customerRepository.AddAsync(customer, cancellationToken);

            return Result<CustomerId>.CreateSuccessful(customer.Id);
        }
    }
}
