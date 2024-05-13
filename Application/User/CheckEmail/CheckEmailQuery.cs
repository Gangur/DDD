using Application.Abstraction;

namespace Application.User.CheckEmail
{
    public record CheckEmailQuery(string Email) : IQuery<bool>
    {
    }
}
