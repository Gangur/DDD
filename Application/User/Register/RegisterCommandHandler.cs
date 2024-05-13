using Application.Abstraction;
using Application.Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Presentation;

namespace Application.User.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser()
            {
                Email = request.Email,
                UserName = request.Email,
                DisplayName = request.DisplayName,
                LastLogin = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                return Result<UserDto>
                    .CreateValidationProblem(createResult.Errors.ToDictionary(k => k.Code, v => v.Description));
            }

            var token = _jwtProvider.GenerateToken(user);

            return Result<UserDto>.CreateSuccessful(UserDto.Create(user.Email!, user.UserName!, token));
        }
    }
}
