using Application.Abstraction;
using Application.Data;
using Domain.Customers;
namespace Application.Customers.Create
{
    internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create(request.Email, request.Name);

            await _customerRepository.AddAsync(customer, cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
