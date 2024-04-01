using Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

                config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });

            return services;
        }
    }
}
