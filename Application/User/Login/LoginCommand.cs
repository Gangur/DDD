using Application.Abstraction;
using Domain.Orders;
using Presentation;

namespace Application.User.Login
{
    public record LoginCommand(string Login, string Password, OrderId? OrderId) : ICommand<UserDto>
    {
    }
}
