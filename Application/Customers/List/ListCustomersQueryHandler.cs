﻿using Application.Abstraction;
using Application.Data;
using Domain.Abstraction.Transport;
using Domain.Customers;
using Presentation;

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

            var listResult = ListResultDto<CustomerDto>.Create(customersTotal, customers
                    .Select(CustomerDto.Map)
                    .ToList());

            return Result<ListResultDto<CustomerDto>>.CreateSuccessful(listResult);
        }
    }
}
