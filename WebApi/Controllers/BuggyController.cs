using Application.Data;
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
            => ActionFromResult(Result.CreateNotFount("Not fount!"));

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
            => ActionFromResult(Result.CreateBadRequest("This is a bad request"));

        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
            => ActionFromResult(Result.CreateUnauthorized());

        [HttpGet("validation-problem")]
        public ActionResult GetValidationProblem()
            => ActionFromResult(Result.CreateValidationProblem(new Dictionary<string, string>()
            {
                { "Problem 1", "This is validation problem 1" },
                { "Problem 2", "This is validation problem 2" }
            }));

        [HttpGet("server-error")]
        public ActionResult GetServerError()
            => throw new Exception("This is a server error");
    }
}
