using Application.Abstraction;
using MediatR;

namespace Application.Behaviours
{
    public sealed class UnitOfWorkBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestType = request.GetType();
            if (requestType.IsSubclassOf(typeof(IDatabaseCommand)) ||
                requestType.IsSubclassOf(typeof(IDatabaseCommand<>)))
            {
                var result = await next();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result;
            }

            return await next();
        }
    }
}
