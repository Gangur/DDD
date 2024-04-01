using Application.Customers.Create;
using Application.Customers.Get;
using Application.Customers.List;
using Application.Data;
using Asp.Versioning;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("customer/v{version:apiVersion}")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<Result> CreateAsync(string email, string name)
        {
            var command = new CreateCustomerCommand(email, name);

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("get")]
        public async Task<Result<Customer>> GetAsync(Guid id)
        {
            var query = new GetCustomerQuery(new CustomerId(id));

            var result = await _mediator.Send(query);

            return result;
        }

        [HttpGet("list")]
        public async Task<Result<IReadOnlyCollection<Customer>>> ListAsync()
        {
            var query = new ListCustomersQuery();

            var result = await _mediator.Send(query);

            return result;
        }
    }
}
