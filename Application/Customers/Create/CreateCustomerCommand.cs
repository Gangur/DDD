using Application.Abstraction;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand(string Email, string Name) : IDatabaseCommand;
}
