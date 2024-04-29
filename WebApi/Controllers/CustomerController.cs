using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.Get;
using Application.Customers.List;
using Application.Customers.Update;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.ComponentModel.DataAnnotations;
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
        public async Task<ActionResult<Guid>> CreateAsync(CancellationToken cancellationToken)
        {
            var command = new CreateCustomerCommand();

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromIdResult(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken)
        {
            var command = new UpdateCustomerCommand(customerDto.Email, customerDto.Name);

            var result = await _mediator.Send(command, cancellationToken);

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
