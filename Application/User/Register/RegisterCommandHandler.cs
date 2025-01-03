﻿using Application.Abstraction;
using Application.Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Presentation;

namespace Application.User.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly ISmsService _smsService;

        public RegisterCommandHandler(UserManager<AppUser> userManager, 
            IJwtProvider jwtProvider, 
            ISmsService smsService)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _smsService = smsService;
        }

        public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser()
            {
                Email = request.Email,
                UserName = request.Email,
                DisplayName = request.DisplayName
            };

            user.UpDateLastLogin();

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                return Result<UserDto>
                    .CreateValidationProblem(createResult.Errors.ToDictionary(k => k.Code, v => v.Description));
            }

            var token = _jwtProvider.GenerateToken(user);

#if DEBUG
            //await _smsService.SendAsync($"User {user.UserName} has been created!",
            //    new string[] { "+1(111)-111-1111" },
            //    cancellationToken);
#else
            _ = _smsService.SendAsync($"User {user.UserName} has been created!", 
                new string[] { user.PhoneNumber! }, 
                cancellationToken);
#endif

            return Result<UserDto>.CreateSuccessful(UserDto.Create(user.Email!, user.UserName!, token));
        }
    }
}
