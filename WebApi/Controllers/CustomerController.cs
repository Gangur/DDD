using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.Get;
using Application.Customers.List;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Presentation;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WebApi.Abstraction;

namespace WebApi.Controllers
{
    [Route("v{version:apiVersion}/customeres")]
    public class CustomerController : BaseApiV1Controller
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateAsync(
            [Required] string email,
            [Required] string name,
            CancellationToken cancellationToken)
        {
            var command = new CreateCustomerCommand(email, name);
            var result = await _mediator.Send(command, cancellationToken);

            Response.Cookies.Append("customer-id", 
                result.Value.Value.ToString(), 
                new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddDays(14)
            });

            return ActionFromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetAsync(
            [Required] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetCustomerQuery(new CustomerId(id));

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IReadOnlyCollection<CustomerDto>>> ListAsync(CancellationToken cancellationToken)
        {
            var query = new ListCustomersQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(
            [Required] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteCustomerCommand(new CustomerId(id));

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromResult(result);
        }
    }
}
