﻿using Application.Products.Create;
using Application.Products.Get;
using Application.Products.List;
using Application.Products.ListBrands;
using Domain.Data;
using Domain.Products;
using Domain.Products.Transport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Presentation.Adstraction;
using WebApi.Abstraction;

namespace WebApi.Controllers
{
    [Route("v{version:apiVersion}/products")]
    public class ProductController : BaseApiV1Controller
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateAsync(string name,
            string priceCurrency,
            string brandName,
            decimal priceAmount,
            string pictureName,
            string sku,
            Category category,
            CancellationToken cancellationToken)
        {
            var price = Money.Create(priceCurrency, priceAmount);
            if (price is null)
                return ValidationProblem("Price data is invalid!");

            var skuObj = Sku.Create(sku);
            if (skuObj is null)
                return ValidationProblem($"{nameof(Sku)} data is invalid!");

            var brand = Brand.Create(brandName);
            if (brand is null)
                return ValidationProblem($"{nameof(Brand)} data is invalid!");

            var command = new CreateProductCommand(name,
                brand,
                pictureName,
                price,
                skuObj,
                category);

            var result = await _mediator.Send(command, cancellationToken);

            return ActionFromIdResult(result);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductQuery(new ProductId(id));

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ListResultDto<ProductDto>>> ListAsync(
            [FromQuery] ProductsListParameters parameters, 
            CancellationToken cancellationToken)
        {
            var query = new ListProductsQuery(parameters);

            var result = await _mediator.Send(query, cancellationToken);

            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Domain = "localhost",
                Expires = DateTime.UtcNow.AddDays(14)
            };

            Response.Cookies.Append("testCookie", "Cookie content", cookieOptions);

            return ActionFromResult(result);
        }

        [HttpGet("list-brands")]
        public async Task<ActionResult<string[]>> ListBrandsAsync(CancellationToken cancellationToken)
        {
            var query = new ListBrandsQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return ActionFromResult(result);
        }
    }
}
