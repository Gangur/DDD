using EmailsSender.Abstractions;
using EmailsSender.Configurations;
using EmailsSender.Consumers;
using EmailsSender.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    private static async Task Main(string[] args)
    {
        var services = CreateServices();
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.ReceiveEndpoint("customer-deleted-event", e =>
            {
                e.Consumer<CustomerDeletedEventConsumer>();
            });
        });

        await busControl.StartAsync(new CancellationToken());
    }

    private static ServiceProvider CreateServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var senderEmailConfiguration = configuration.GetRequiredSection("Smtp");
        var senderEmailSettings = senderEmailConfiguration.Get<EmailSenderSettings>()!;

        var serviceProvider = new ServiceCollection()
            .AddTransient<IEmailSender>(s => new EmailSender(
                senderEmailSettings.Host,
                senderEmailSettings.UserName,
                senderEmailSettings.Password))
            .BuildServiceProvider();

        return serviceProvider;
    }
}