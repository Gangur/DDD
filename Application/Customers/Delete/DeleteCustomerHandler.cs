using Application.Abstraction;
using Application.Data;
using Domain.Customers;
using IntegrationEvents;

namespace Application.Customers.Delete
{
    public sealed class DeleteCustomerHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventBus _eventBus;

        public DeleteCustomerHandler(ICustomerRepository customerRepository, IEventBus eventBus)
        {
            _customerRepository = customerRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                return Result.CreateFailed("The customer has not been found!");
            }

            _customerRepository.Remove(customer);

            await _eventBus.PublishAsync(
                new CustomerDeletedIntegrationEvent(customer.Email, customer.Name), 
                cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
