using Application.Abstraction;
using Application.Data;
using Domain.Customers;

namespace Application.Customers.Get
{
    internal sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindAsync(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return Result<Customer>.CreateFailed("The customer has not been found!");
            }

            return Result<Customer>.CreateSuccessful(customer);
        }
    }
}
