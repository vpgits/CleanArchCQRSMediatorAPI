// <copyright file="GetAllBooksDtoQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Queries
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    public record GetAllBooksDtoQuery : IQuery<Result> { }
}