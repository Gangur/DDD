using Application.Data;
using MediatR;

namespace Application.Abstraction
{
    public interface IQueryHandler<TQuery, TResponce> : IRequestHandler<TQuery, Result<TResponce>>
        where TQuery : IQuery<TResponce>
    {
    }
}
