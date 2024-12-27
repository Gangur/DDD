using Application.Abstraction;
using Application.Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Presentation;
using Domain.Extensions;
using Domain.Orders;

namespace Application.User.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(
            UserManager<AppUser> userManager,
            IOrderRepository orderRepository,
            IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _jwtProvider = jwtProvider;
        }
        
        public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);

            if (user == null)
            {
                return Result<UserDto>.CreateUnauthorized($"User {request.Login} has not been found!");
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result<UserDto>.CreateUnauthorized();
            }

            var token = _jwtProvider.GenerateToken(user);

            if (request.OrderId.HasValue())
            {
                var order = await _orderRepository.FindAsync(request.OrderId!, cancellationToken);

                if (order != default)
                {
                    order.AssignUser(user);
                }
            }

            user.UpDateLastLogin();

            return Result<UserDto>.CreateSuccessful(UserDto.Create(user.Email!, user.UserName!, token));
        }
    }
}
