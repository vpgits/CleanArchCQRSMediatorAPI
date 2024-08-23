// <copyright file="IQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging
{
    using MediatR;

    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}