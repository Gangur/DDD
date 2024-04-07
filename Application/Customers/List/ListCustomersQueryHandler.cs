using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Presentation;

namespace Application.Customers.List
{
    internal sealed class ListCustomersQueryHandler : IQueryHandler<ListCustomersQuery, IReadOnlyCollection<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public ListCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<IReadOnlyCollection<CustomerDto>>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.ListAsync(cancellationToken);

            return Result<IReadOnlyCollection<CustomerDto>>
                .CreateSuccessful(customers
                    .Select(c => new CustomerDto(c.Id.Value, c.Email, c.Name))
                    .ToList());
        }
    }
}
