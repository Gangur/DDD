using Application.Abstraction;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using static MassTransit.MessageHeaders;

namespace Infrastructure.MessageBroker
{
    public class MessageSender : IMessageSender
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;

        public MessageSender(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
        }

        public Task SendAsync<T>(T message, CancellationToken cancellationToken)
            where T : class
                => Task.Run(() =>
                {
                    var factory = new ConnectionFactory()
                    {
                        Uri = new Uri($"amqp://{_username}:{_password}@{_host}/")
                    };

                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    var ttl = new Dictionary<string, object>
                    {
                        { "x-message-ttl", 30000 }
                    };

                    var messageType = (typeof(T));

                    channel.ExchangeDeclare(messageType.FullName, ExchangeType.Direct, arguments: ttl);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    cancellationToken.ThrowIfCancellationRequested();

                    channel.BasicPublish(messageType.Name, $"{messageType.Name.ToLower()}.init", null, body);
                });
    }
}
