namespace Application.Abstraction
{
    public interface IMessageSender
    {
        public Task SendAsync<T>(T message, CancellationToken cancellationToken) where T : class;
    }
}
