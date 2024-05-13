using Domain.User;

namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        string GenerateToken(AppUser user);
    }
}
