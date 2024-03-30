using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Orders;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<T> GetAll<T>() where T : class => Set<T>();
        private IQueryable<T> GetQuery<T>() where T : class => Set<T>().AsNoTracking();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
