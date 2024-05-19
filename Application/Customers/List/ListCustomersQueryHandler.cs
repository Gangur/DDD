using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Domain.Products;
using Presentation;
using Presentation.Adstraction;

namespace Application.Customers.List
{
    internal sealed class ListCustomersQueryHandler : IQueryHandler<ListCustomersQuery, ListResultDto<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public ListCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<ListResultDto<CustomerDto>>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var customersTotal = await _customerRepository.CountAsync(request.ListParameters, cancellationToken);
            var customers = await _customerRepository.ListAsync(request.ListParameters, cancellationToken);

            var listResult = ListResultDto<CustomerDto>.Create(customersTotal, 
                Array.ConvertAll(customers,
                    new Converter<Customer, CustomerDto>(CustomerDto.Map)));

            return Result<ListResultDto<CustomerDto>>.CreateSuccessful(listResult);
        }
    }
}
