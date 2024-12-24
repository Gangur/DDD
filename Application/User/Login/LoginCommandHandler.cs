using Application.Abstraction;
using Application.Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Presentation;

namespace Application.User.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserDto>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, IJwtProvider jwtProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        
        public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);

            if (user == null)
            {
                return Result<UserDto>.CreateUnauthorized($"User {request.Login} has not been found!");
            }

            var singResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (singResult.IsLockedOut)
            {
                return Result<UserDto>.CreateUnauthorized($"User {request.Login} is locked out!");
            }

            if (!singResult.Succeeded)
            {
                return Result<UserDto>.CreateUnauthorized();
            }

            var token = _jwtProvider.GenerateToken(user);

            user.LastLogin = DateTime.UtcNow;

            return Result<UserDto>.CreateSuccessful(UserDto.Create(user.Email!, user.UserName!, token));
        }
    }
}
