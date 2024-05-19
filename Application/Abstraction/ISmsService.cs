using Application.Data;

namespace Application.Abstraction
{
    public interface ISmsService
    {
        Task SendAsync(string message, string[] recipients, CancellationToken token);
    }
}
