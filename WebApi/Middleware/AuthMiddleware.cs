using Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Persistence.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthMiddleware> _logger;

        public AuthMiddleware(RequestDelegate next,
            ILogger<AuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context,
            AuthService authService)
        {
            var endpoint = context.GetEndpoint();
            bool isAllowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

            if (!isAllowAnonymous)
            {
                var email = context.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

                if (Guid.TryParse(context.User.FindFirst("id")?.Value, out Guid id) && email.HasValue())
                {
                    authService.Init(id, email!);
                }
            }

            await _next(context);
        }
    }
}
