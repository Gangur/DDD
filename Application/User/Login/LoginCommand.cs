using Application.Abstraction;
using Presentation;

namespace Application.User.Login
{
    public record LoginCommand(string Login, string Password) : ICommand<UserDto>
    {
    }
}
