﻿using Application.Abstraction;
using Application.Data;
using Domain.Products;
using Presentation;

namespace Application.Products.Get
{
    internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.TakeAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                return Result<ProductDto>.CreateNotFound("The product has not been found!");
            }

            return Result<ProductDto>.CreateSuccessful(ProductDto.Map(product));
        }
    }
}
