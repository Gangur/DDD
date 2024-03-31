using Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Abstraction;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private IPublisher _publisher;

        public DbSet<T> GetAll<T>() where T : class => Set<T>();
        public IQueryable<T> GetQuery<T>() where T : class => Set<T>().AsNoTracking();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder, IPublisher publisher) : base(optionsBuilder)
        {
            _publisher = publisher;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<Entity<IEntityId>>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .SelectMany(e => e.DomainEvents);

            var result = await base.SaveChangesAsync(cancellationToken);

            await Task.WhenAll(domainEvents.Select(x => _publisher.Publish(x, cancellationToken)));

            return result;
        }
    }
}
