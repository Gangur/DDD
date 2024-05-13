using WebApi.Configurations;

namespace WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureOptions<SwaggerGenOptionsSetup>();
            services.ConfigureOptions<SwaggerUIOptionsSetup>();

            return services;
        }
    }
}
