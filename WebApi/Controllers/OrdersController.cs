using Application.Orders.AddLineItem;
using Application.Orders.Create;
using Application.Orders.Get;
using Application.Orders.List;
using Application.Orders.RemoveLineItem;
using Domain.Abstraction.Transport;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Presentation.Adstraction;
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
        public async Task<ActionResult<Guid>> CreateAsync(
            [Required] Guid customerId,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(new CustomerId(customerId));

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromIdResult(result);
        }

        [HttpDelete("remove-line-item")]
        public async Task<ActionResult<OrderDto>> RemoveLineItemAsync(
            [Required] Guid orderId,
            [Required] Guid productId,
            [Required] int quantity,
            CancellationToken cancellationToken)
        {
            var command = new RemoveLineItemCommand(new OrderId(orderId), new ProductId(productId), quantity);

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpPost("add-line-item")]
        public async Task<ActionResult<OrderDto>> AddLineItemAsync(
            [Required] Guid orderId,
            [Required] Guid productId,
            [Required] int quantity,
            CancellationToken cancellationToken)
        {
            var command = new AddLineItemCommand(new OrderId(orderId), new ProductId(productId), quantity);

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

        [HttpGet("by-customer/{customerId}")]
        public async Task<ActionResult<OrderDto>> GetIncompletedIfExistAsync([Required] Guid customerId, CancellationToken cancellationToken)
        {
            var query = new GetOrderQuery(new CustomerId(customerId));

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ListResultDto<OrderDto>>> ListAsync(
            [FromQuery] ListParameters parameters, CancellationToken cancellationToken)
        {
            var query = new ListOrdersQuery(parameters);

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }
    }
}
