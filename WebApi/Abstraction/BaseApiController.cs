using Application.Data;
using Application.Enums;
using Domain.Abstraction;
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
                return HandelErrorResult(result.Type, result.ValidationProblems!, result.ErrorMessage!);
        }

        protected ActionResult ActionFromResult<T>(Result<T> result)
        {
            if (result.Success)
            {
                var idResult = result.Value as IEntityId;

                if (idResult != default)
                    return Ok(idResult.Value);
                else
                    return Ok(result.Value);
            }
            else
                return HandelErrorResult(result.Type, result.ValidationProblems!, result.ErrorMessage!);
        }

        protected ActionResult ActionFromIdResult<T>(Result<T> result) where T : IEntityId
        {
            if (result.Success)
            {
                return Ok(result.Value.Value);
            }
            else
                return HandelErrorResult(result.Type, result.ValidationProblems!, result.ErrorMessage!);
        }

        private ActionResult HandelErrorResult(ResultType resultType, Dictionary<string, string> validationProblems, string error)
            => resultType switch
            {
                ResultType.NotFount => NotFound(error),
                ResultType.Unauthorized => Unauthorized(error),
                ResultType.BadRequest => HandelBadRequest(validationProblems, error),
                _ => throw new NotImplementedException(),
            };

        private ActionResult HandelBadRequest(Dictionary<string, string> validationProblems, string error)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                foreach (var item in validationProblems)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return ValidationProblem();
            }
            else
                return BadRequest(error);
        }
    }
}
