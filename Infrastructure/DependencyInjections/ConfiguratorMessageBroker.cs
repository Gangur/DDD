using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.DependencyInjections
{
    public static class ConfiguratorMessageBroker
    {
        public static MessageBrokerSettings ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            var messageBrokerConfiguration = configuration.GetRequiredSection("MessageBroker");
            services.Configure<MessageBrokerSettings>(messageBrokerConfiguration);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
            return messageBrokerConfiguration.Get<MessageBrokerSettings>()!;
        }
    }
}
