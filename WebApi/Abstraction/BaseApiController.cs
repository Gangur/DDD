using Application.Data;
using Application.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstraction
{
    [ApiController]
    public abstract class BaseApiController : Controller
    {
        protected readonly IMediator _mediator;
        public BaseApiController(IMediator mediator) => _mediator = mediator;

        protected ActionResult ActionFromResult(Result result)
        {
            if (result.Success)
                return Ok();
            else
                return HandelErrorResult(result.Type, result.ErrorMessage);
        }

        protected ActionResult ActionFromResult<T>(Result<T> result)
        {
            if (result.Success)
                return Ok(result.Value);
            else
                return HandelErrorResult(result.Type, result.ErrorMessage);
        }

        private ActionResult HandelErrorResult(ResultType resultType, string error)
            => resultType switch
            {
                ResultType.NotFount => NotFound(error),
                ResultType.Unauthorized => Unauthorized(error),
                ResultType.ValidationProblem => ValidationProblem(error),
                _ => throw new NotImplementedException(),
            };
    }
}
