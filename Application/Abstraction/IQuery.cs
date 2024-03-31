using Application.Data;
using MediatR;

namespace Application.Abstraction
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
