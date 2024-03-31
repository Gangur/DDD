using Application.Customers.Get;
using Application.Customers.List;
using Application.Data;
using Application.Orders.Create;
using Application.Orders.Get;
using Application.Orders.List;
using Application.Orders.RemoveLineItem;
using Domain.Customers;
using Domain.LineItems;
using Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<Result> CreateAsync(Guid customerId)
        {
            var command = new CreateOrderCommand(new CustomerId(customerId));

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpDelete("remove-line-item")]
        public async Task<Result> RemoveLineItemAsync(Guid orderId, Guid lineItemId)
        {
            var command = new RemoveLineItemCommand(new OrderId(orderId), new LineItemId(lineItemId));

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("get")]
        public async Task<Result<Order>> GetAsync(Guid id)
        {
            var query = new GetOrderQuery(new OrderId(id));

            var result = await _mediator.Send(query);

            return result;
        }

        [HttpGet("list")]
        public async Task<Result<IReadOnlyCollection<Order>>> ListAsync()
        {
            var query = new ListOrdersQuery();

            var result = await _mediator.Send(query);

            return result;
        }
    }
}
