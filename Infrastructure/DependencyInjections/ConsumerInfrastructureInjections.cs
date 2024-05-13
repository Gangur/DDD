using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public static class ConsumerInfrastructureInjections
    {
        public static IServiceCollection AddConsumerInfrastructure(this IServiceCollection services,
            IConfiguration configuration, Type[] consumers)
        {
            var messageBrokerSettings = services.ConfigureMessageBroker(configuration);

            services.AddMassTransit(busConfiguratior =>
            {
                busConfiguratior.SetKebabCaseEndpointNameFormatter();

                for (int i = 0; i < consumers.Length; i++)
                {
                    busConfiguratior.AddConsumer(consumers[i]);
                }

                busConfiguratior.UsingRabbitMq((context, configuratior) =>
                {
                    configuratior.Host(new Uri("amqp://" + messageBrokerSettings.Host), h =>
                    {
                        h.Username(messageBrokerSettings.Username);
                        h.Password(messageBrokerSettings.Password);
                    });

                    configuratior.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
