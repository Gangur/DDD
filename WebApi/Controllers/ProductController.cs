﻿using Application.Data;
using Application.Products.Create;
using Application.Products.Get;
using Application.Products.List;
using Asp.Versioning;
using Domain.Data;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("product/v{version:apiVersion}")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<Result> CreateAsync(string name, string priceCurrency, decimal priceAmount, string sku)
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

        [HttpGet("get")]
        public async Task<Result<Product>> GetAsync(Guid id)
        {
            var query = new GetProductQuery(new ProductId(id));

            var result = await _mediator.Send(query);

            return result;
        }

        [HttpGet("list")]
        public async Task<Result<IReadOnlyCollection<Product>>> ListAsync()
        {
            var query = new ListProductsQuery();

            var result = await _mediator.Send(query);

            return result;
        }
    }
}
