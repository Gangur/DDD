using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers
{
    [Route("v{version:apiVersion}/buggy")]
    public class BuggyController : BaseApiV1Controller
    {
        public BuggyController() : base(default)
        {
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
            => NotFound();

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
            => BadRequest(new ProblemDetails() { Title = "This is a bad request" });

        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
            => Unauthorized();

        [HttpGet("validation-problem")]
        public ActionResult GetValidationProblem()
        {
            ModelState.AddModelError("Problem 1", "This is validation problem 1");
            ModelState.AddModelError("Problem 2", "This is validation problem 2");
            return ValidationProblem();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
            => throw new Exception("This is a server error");
    }
}
