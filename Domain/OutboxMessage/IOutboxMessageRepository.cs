namespace Domain.OutboxMessage
{
    public interface IOutboxMessageRepository
    {
        Task<List<OutboxMessage>> ResiveUnprocessedMessagesAsync(int take, string type, CancellationToken cancellationToken);

        Task CompleteAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken);

        Task SetErrorMessage(OutboxMessage outboxMessage, string error, CancellationToken cancellationToken);
    }
}
