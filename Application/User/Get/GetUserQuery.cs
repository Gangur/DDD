using Application.Abstraction;
using Presentation;

namespace Application.User.Get
{
    public record GetUserQuery(string Email) : IQuery<UserDto>
    {
    }
}
