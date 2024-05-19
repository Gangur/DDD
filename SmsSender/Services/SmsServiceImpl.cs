using Grpc.Core;
using SmsGRpc;
using static SmsGRpc.SmsService;

namespace SmsSender.Services
{
    public class SmsServiceImpl : SmsServiceBase
    {
        public override Task<RpcResult> Send(SmsRequest request, 
            ServerCallContext context)
        {
            for (int i = 0; i < request.Recipients.Count; i++)
            {
                Console.WriteLine(request.Message); // Send sms to recipient
            }

            return Task.FromResult(new RpcResult()
            {
                Success = true,
                Message = "Sms successfully sent!"
            });
        }
    }
}
