using Application.Abstraction;
using Application.Data;
using Domain.Customers;

namespace Application.Customers.List
{
    internal sealed class ListCustomersQueryHandler : IQueryHandler<ListCustomersQuery, IReadOnlyCollection<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public ListCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<IReadOnlyCollection<Customer>>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var products = await _customerRepository.ListAsync(cancellationToken);

            return Result<IReadOnlyCollection<Customer>>.CreateSuccessful(products);
        }
    }
}
