using Application.Orders.Create;
using Application.Orders.Get;
using Application.Orders.List;
using Application.Orders.RemoveLineItem;
using Domain.Customers;
using Domain.LineItems;
using Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.ComponentModel.DataAnnotations;
using WebApi.Abstraction;
namespace WebApi.Controllers
{
    [Route("v{version:apiVersion}/orders")]
    public class OrdersController : BaseApiV1Controller
    {
        public OrdersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(
            [Required] Guid customerId,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(new CustomerId(customerId));

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpDelete("remove-line-item")]
        public async Task<ActionResult> RemoveLineItemAsync(
            [Required] Guid orderId,
            [Required] Guid lineItemId,
            CancellationToken cancellationToken)
        {
            var command = new RemoveLineItemCommand(new OrderId(orderId), new LineItemId(lineItemId));

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetAsync(
            [Required] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetOrderQuery(new OrderId(id));

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IReadOnlyCollection<OrderDto>>> ListAsync(CancellationToken cancellationToken)
        {
            var query = new ListOrdersQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }
    }
}
