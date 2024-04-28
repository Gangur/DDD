using Domain.OutboxMessage;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public OutboxMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CompleteAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken)
        {
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<OutboxMessage>> ResiveUnprocessedMessagesAsync(int take, string type, CancellationToken cancellationToken)
            => _context.GetSet<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .Where(m => m.Type == type)
                .Take(take)
                .ToListAsync(cancellationToken);

        public Task SetErrorMessage(OutboxMessage outboxMessage, string error, CancellationToken cancellationToken)
        {
            outboxMessage.Error = error;
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
