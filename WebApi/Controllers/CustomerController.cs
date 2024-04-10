using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.Get;
using Application.Customers.List;
using Application.Data;
using Asp.Versioning;
using Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.ComponentModel.DataAnnotations;

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
        public async Task<Result> CreateAsync(
            [Required] string email, 
            [Required] string name, 
            CancellationToken cancellationToken)
        {
            var command = new CreateCustomerCommand(email, name);

            var result = await _mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpGet("get")]
        public async Task<Result<CustomerDto>> GetAsync(
            [Required] Guid id, 
            CancellationToken cancellationToken)
        {
            var query = new GetCustomerQuery(new CustomerId(id));

            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }

        [HttpGet("list")]
        public async Task<Result<IReadOnlyCollection<CustomerDto>>> ListAsync(CancellationToken cancellationToken)
        {
            var query = new ListCustomersQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteAsync(
            [Required] Guid id, 
            CancellationToken cancellationToken)
        {
            var command = new DeleteCustomerCommand(new CustomerId(id));

            var result = await _mediator.Send(command, cancellationToken);

            return result;
        }
    }
}
