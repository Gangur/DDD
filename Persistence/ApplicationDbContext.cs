using Application.Abstraction;
using Domain.Abstraction;
using Domain.OutboxMessage;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Persistence
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        private IPublisher _publisher;

        public DbSet<T> GetSet<T>() where T : class => Set<T>();
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any());

            var events = entities
                .SelectMany(entities =>
                {
                    var domainEvents = entities.DomainEvents;

                    entities.ClearDomainEvents();

                    return domainEvents;
                }).Select(de => new OutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    OccurredOnUtc = DateTime.UtcNow,
                    Type = de.GetType().Name,
                    Content = JsonConvert.SerializeObject(de, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    })
                }).ToList();

            await GetSet<OutboxMessage>().AddRangeAsync(events, cancellationToken);

            var result = await base.SaveChangesAsync(cancellationToken);

            await Task.WhenAll(entities
                .SelectMany(e => e.DomainEvents)
                .Select(x => _publisher.Publish(x, cancellationToken)));

            return result;
        }
    }
}
