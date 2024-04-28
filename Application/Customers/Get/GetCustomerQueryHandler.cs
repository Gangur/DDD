using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Presentation;

namespace Application.Customers.Get
{
    internal sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.TakeAsync(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return Result<CustomerDto>.CreateNotFount("The customer has not been found!");
            }

            return Result<CustomerDto>.CreateSuccessful(new CustomerDto(customer.Id.Value, customer.Email, customer.Name));
        }
    }
}
