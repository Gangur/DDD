using Application.User.CheckEmail;
using Application.User.Get;
using Application.User.Login;
using Application.User.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.Security.Claims;
using WebApi.Abstraction;

namespace WebApi.Controllers
{
    [Route("v{version:apiVersion}/auth")]
    public class AuthController : BaseApiV1Controller
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == default)
            {
                return Unauthorized();
            }

            var query = new GetUserQuery(email);

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(registerDto.Email, registerDto.DisplayName, registerDto.Password);

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var query = new LoginCommand(loginDto.Login, loginDto.Password);

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<bool>> CheckEmailAsync(string email, CancellationToken cancellationToken)
        {
            var query = new CheckEmailQuery(email);

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }
    }
}
