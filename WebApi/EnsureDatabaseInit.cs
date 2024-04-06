using Persistence;
using Persistence.Data;

namespace WebApi
{
    public static class EnsureDatabaseInit
    {
        public static void EnsureCreated(ApplicationDbContext dbContext)
        {
#if DEBUG
            dbContext.Database.EnsureCreated();
            DbInitializer.Initialize(dbContext);
#endif
        }
    }
}