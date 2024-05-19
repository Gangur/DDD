using Grpc.Core;
using Microsoft.Extensions.Configuration;
using SmsGRpc;
using SmsSender.Services;

namespace SmsSender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configurations = BuildConfiguration(args);
            var grpcConf = configurations.GetRequiredSection("GRPC");

            string host = grpcConf["Host"]!;
            string port = grpcConf["Port"]!;

            ArgumentException.ThrowIfNullOrWhiteSpace(host);
            ArgumentException.ThrowIfNullOrWhiteSpace(port);

            var server = new Server()
            {
                Ports = { new ServerPort(host, Convert.ToInt32(port), ServerCredentials.Insecure) },
                Services = {
                    SmsService.BindService(new SmsServiceImpl())
                }
            };

            try
            {
                server.Start();
                Console.WriteLine($"Server is listening to port {port}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (server != null)
                {
                    await server.ShutdownAsync();
                }
            }
        }

        private static IConfiguration BuildConfiguration(string[] args)
            => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
    }
}
