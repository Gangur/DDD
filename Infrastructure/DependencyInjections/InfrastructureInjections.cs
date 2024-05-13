using Application.Abstraction;
using Azure.Storage.Blobs;
using Infrastructure.Blobs;
using Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public static class InfrastructureInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var messageBrokerSettings = services.ConfigureMessageBroker(configuration);

            services.AddMassTransit(busConfiguratior =>
            {
                busConfiguratior.SetKebabCaseEndpointNameFormatter();

                busConfiguratior.UsingRabbitMq((context, configuratior) =>
                {
                    configuratior.Host(new Uri("amqp://" + messageBrokerSettings.Host), h =>
                    {
                        h.Username(messageBrokerSettings.Username);
                        h.Password(messageBrokerSettings.Password);
                    });
                });
            });

            services.AddTransient<IMessageSender>(p =>
                new MessageSender(
                    messageBrokerSettings.Host,
                    messageBrokerSettings.Username,
                    messageBrokerSettings.Password));
            services.AddTransient<IEventBus, EventBus>();

            var blobsConnectionString = configuration.GetConnectionString("StorageAccount")!;
            services.AddSingleton(x => new BlobServiceClient(blobsConnectionString));
            services.AddSingleton<IBlobService, BlobService>();

            return services;
        }
    }
}
