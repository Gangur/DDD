﻿using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using Domain.Orders;

namespace Application.Orders.Create
{
    internal sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand, OrderId>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindAsync(request.customerId, cancellationToken);

            if (customer is null)
            {
                return Result<OrderId>.CreateValidationProblem("The customer has not been found!");
            }

            var order = Order.Create(customer);

            await _orderRepository.AddAsync(order, cancellationToken);

            return Result<OrderId>.CreateSuccessful(order.Id);
        }
    }
}
