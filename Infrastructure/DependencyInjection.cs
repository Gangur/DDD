using Application.Abstraction;
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
            var settings = services.ConfigureMessageBroker(configuration);

            services.AddMassTransit(busConfiguratior =>
            {
                busConfiguratior.SetKebabCaseEndpointNameFormatter();

                busConfiguratior.UsingRabbitMq((context, configuratior) =>
                {
                    configuratior.Host(new Uri("amqp://" + settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                });
            });

            services.AddTransient<IMessageSender>(p =>
                new MessageSender(
                    settings.Host,
                    settings.Username,
                    settings.Password));
            services.AddTransient<IEventBus, EventBus>();

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
                });
            });

            return services;
        }
    }
}
