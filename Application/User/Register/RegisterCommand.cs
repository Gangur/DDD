using Application.Abstraction;
using Presentation;

namespace Application.User.Register
{
    public record RegisterCommand(string Email, string DisplayName, string Password) : ICommand<UserDto>
    {
    }
}
