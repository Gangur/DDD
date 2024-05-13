using Application.Abstraction;
using Application.Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Presentation;

namespace Application.User.Get
{
    internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public GetUserQueryHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<UserDto>.CreateBadRequest($"User {request.Email} has not been found!");
            }

            var token = _jwtProvider.GenerateToken(user);

            return Result<UserDto>.CreateSuccessful(UserDto.Create(user.Email!, user.DisplayName, token));
        }
    }
}
