using Application.Data;
using Application.Orders.Create;
using Application.Orders.RemoveLineItem;
using Domain.Customers;
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
        public async Task<Result> Create(Guid customerId)
        {
            var command = new CreateOrderCommand(new CustomerId(customerId));

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost("remove-order")]
        public async Task<Result> RemoveOrderAsync(Guid orderId, Guid lineItemId)
        {
            var command = new RemoveLineItemCommand(new OrderId(orderId), new LineItemId(lineItemId));

            var result = await _mediator.Send(command);

            return result;
        }
    }
}
