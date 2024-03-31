using Application.Data;
using Application.Products.Create;
using Domain.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<Result> Create(string name, string priceCurrency, decimal priceAmount, string sku)
        {
            var price = Money.Create(priceCurrency, priceAmount);
            if (price is null)
                return Result.CreateFailed("Price data is invalid!");

            var skuObj = Sku.Create(sku);
            if (skuObj is null)
                return Result.CreateFailed("Sku data is invalid!");

            var command = new CreateProductCommand(name,
                price,
                skuObj);

            var result = await _mediator.Send(command);

            return result;
        }
    }
}
