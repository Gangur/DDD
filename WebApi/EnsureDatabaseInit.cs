using Persistence;
using Persistence.Data;

namespace WebApi
{
    public static class EnsureDatabaseInit
    {
        public static void EnsureCreated(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.EnsureCreated())
            {
                DbInitializer.Initialize(context);
            }
        }
    }
}