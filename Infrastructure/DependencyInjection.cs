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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var messageBrokerConfiguration = configuration.GetRequiredSection("MessageBroker");
            services.Configure<MessageBrokerSettings>(messageBrokerConfiguration);

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            services.AddMassTransit(busConfiguratior =>
            {
                busConfiguratior.SetKebabCaseEndpointNameFormatter();

                busConfiguratior.UsingRabbitMq((context, configuratior) =>
                {
                    var settings = context.GetRequiredService<MessageBrokerSettings>();

                    configuratior.Host(new Uri("amqp://" + settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                });
            });

            var settings = messageBrokerConfiguration.Get<MessageBrokerSettings>()!;

            services.AddTransient<IMessageSender>(p => 
                new MessageSender(
                    settings.Host,
                    settings.Username,
                    settings.Password));
            services.AddTransient<IEventBus, EventBus>();

            return services;
        }
    }
}
