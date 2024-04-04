using Domain.Abstraction;
using Domain.OutboxMessage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Persistence.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result, CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext is null)
                return base.SavedChangesAsync(eventData, result, cancellationToken);

            var events = dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
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

            dbContext.Set<OutboxMessage>().AddRange(events);

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
