﻿using Application.Data;
using MediatR;

namespace Application.Abstraction
{
    public interface IDatabaseCommand : ICommand
    {
    }

    public interface IDatabaseCommand<TResponse> : ICommand<TResponse>
    {

    }
}