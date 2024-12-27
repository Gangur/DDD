using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApi.Middleware
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IServiceProvider serviceProvider)
        {
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var transaction = await applicationDbContext.Database.BeginTransactionAsync(context.RequestAborted);
            try
            {
                await _next(context);
                await transaction.CommitAsync(context.RequestAborted);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
