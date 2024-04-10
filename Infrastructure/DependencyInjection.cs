using Application.Abstraction;
using Azure.Storage.Blobs;
using Infrastructure.Blobs;
using Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        private static MessageBrokerSettings ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            var messageBrokerConfiguration = configuration.GetRequiredSection("MessageBroker");
            services.Configure<MessageBrokerSettings>(messageBrokerConfiguration);

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            return messageBrokerConfiguration.Get<MessageBrokerSettings>()!;
        }

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

        public static IServiceCollection AddConsumerInfrastructure(this IServiceCollection services,
            IConfiguration configuration, Type[] consumers)
        {
            var settings = services.ConfigureMessageBroker(configuration);

            services.AddMassTransit(busConfiguratior =>
            {
                busConfiguratior.SetKebabCaseEndpointNameFormatter();

                for (int i = 0; i < consumers.Length; i++)
                {
                    busConfiguratior.AddConsumer(consumers[i]);
                }

                busConfiguratior.UsingRabbitMq((context, configuratior) =>
                {
                    configuratior.Host(new Uri("amqp://" + settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });

                    configuratior.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
