﻿using Application.Abstraction;
using Application.Data;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.RemoveLineItem
{
    internal sealed class RemoveLineItemHandler : ICommandHandler<RemoveLineItemCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveLineItemHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindWithIncludedLineItemAsync(request.OrderId, 
                request.LineItemId, 
                cancellationToken);

            if (order is null)
            {
                return Result.CreateFailed("The order has not been found!");
            }

            order.RemoveLineItem(request.LineItemId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.CreateSuccessful();
        }
    }
}
