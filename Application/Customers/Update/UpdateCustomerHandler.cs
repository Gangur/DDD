using Application.Abstraction;
using Application.Data;

namespace Application.Customers.Update
{
    public class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand>
    {
        public Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
