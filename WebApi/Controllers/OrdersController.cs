using Application.Data;
using Application.Orders.Create;
using Application.Orders.Get;
using Application.Orders.List;
using Application.Orders.RemoveLineItem;
using Asp.Versioning;
using Domain.Customers;
using Domain.LineItems;
using Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("orders/v{version:apiVersion}")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<Result> CreateAsync(
            [Required] Guid customerId, 
            CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(new CustomerId(customerId));

            var result = await _mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpDelete("remove-line-item")]
        public async Task<Result> RemoveLineItemAsync(
            [Required] Guid orderId, 
            [Required] Guid lineItemId, 
            CancellationToken cancellationToken)
        {
            var command = new RemoveLineItemCommand(new OrderId(orderId), new LineItemId(lineItemId));

            var result = await _mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpGet("get")]
        public async Task<Result<OrderDto>> GetAsync(
            [Required] Guid id, 
            CancellationToken cancellationToken)
        {
            var query = new GetOrderQuery(new OrderId(id));

            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }

        [HttpGet("list")]
        public async Task<Result<IReadOnlyCollection<OrderDto>>> ListAsync(CancellationToken cancellationToken)
        {
            var query = new ListOrdersQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }
    }
}
