using Application.Abstraction;

namespace Application.Customers.Update
{
    public record UpdateCustomerCommand(string Email, string Name) : ICommand;
}
