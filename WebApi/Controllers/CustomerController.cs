using Application.Customers.Create;
using Application.Customers.Get;
using Application.Customers.List;
using Application.Data;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("customer")]
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
