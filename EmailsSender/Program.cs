using EmailsSender.Abstractions;
using EmailsSender.Configurations;
using EmailsSender.Consumers;
using EmailsSender.Services;
using Infrastructure;
using Infrastructure.DependencyInjections;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();

                if (args != null)
                    config.AddCommandLine(args);
            }).ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;

                var senderEmailConfiguration = configuration.GetRequiredSection("Smtp");
                var senderEmailSettings = senderEmailConfiguration.Get<EmailSenderSettings>()!;

                services.AddTransient<IEmailSender>(s => new EmailSender(
                    senderEmailSettings.Host,
                    senderEmailSettings.UserName,
                    senderEmailSettings.Password))
                .AddConsumerInfrastructure(configuration, new[]
                {
                    typeof(CustomerDeletedEventConsumer)
                });
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
            });

        await builder.UseWindowsService().Build().RunAsync();
    }
}