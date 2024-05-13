using Application.Abstraction;
using Application.Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.CheckEmail
{
    internal class CheckEmailQueryHandler : IQueryHandler<CheckEmailQuery, bool>
    {
        private readonly UserManager<AppUser> _userManager;
        public CheckEmailQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<bool>> Handle(CheckEmailQuery request, CancellationToken cancellationToken)
        {
            return Result<bool>.CreateSuccessful(await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken));
        }
    }
}
