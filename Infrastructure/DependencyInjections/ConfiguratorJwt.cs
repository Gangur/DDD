using Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.DependencyInjections
{
    public static class ConfiguratorJwt
    {
        public static JwtSettings ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection("JWT");
            services.Configure<JwtSettings>(settings);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);
            return settings.Get<JwtSettings>()!;
        }
    }
}
