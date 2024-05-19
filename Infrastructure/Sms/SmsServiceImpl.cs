using Application.Abstraction;
using Grpc.Core;
using Grpc.Net.Client;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using SmsGRpc;

namespace Infrastructure.Sms
{
    internal class SmsServiceImpl : ISmsService
    {
        private readonly GrpcChannel _channel;
        private readonly SmsService.SmsServiceClient _client;
        public SmsServiceImpl(IOptions<GrpcSettings> options)
        {
            var settings = options.Value;
            _channel = GrpcChannel.ForAddress($"http://{settings.Host}:{settings.Port}");
            _client = new SmsService.SmsServiceClient(_channel);
        }

        public async Task SendAsync(string message, string[] recipients, CancellationToken token)
        {
            var request = new SmsRequest()
            {
                Message = message,
            };
            request.Recipients.AddRange(recipients);

            var result = await _client.SendAsync(request, cancellationToken: token);

            if (!result.Success)
            {
                throw new RpcException(Status.DefaultSuccess, result!.Message);
            }
        }
    }
}
