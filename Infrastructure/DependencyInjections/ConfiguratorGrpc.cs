using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.DependencyInjections
{
    public static class ConfiguratorGrpc
    {
        public static GrpcSettings ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection("GRPC");
            services.Configure<GrpcSettings>(settings);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<GrpcSettings>>().Value);
            return settings.Get<GrpcSettings>()!;
        }
    }
}
