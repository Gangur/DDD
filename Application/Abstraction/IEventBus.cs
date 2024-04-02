namespace Application.Abstraction
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken)
             where T : class;

        Task PublishForAzureFunctionAsync<T>(T message, CancellationToken cancellationToken)
             where T : class;
    }
}
