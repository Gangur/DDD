using Application.Customers.Create;
using Application.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<Result> Create(string email, string name)
        {
            var command = new CreateCustomerCommand(email, name);

            var result = await _mediator.Send(command);

            return result;
        }
    }
}
