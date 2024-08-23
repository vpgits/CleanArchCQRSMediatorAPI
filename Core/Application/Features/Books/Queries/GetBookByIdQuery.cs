// <copyright file="GetBookByIdQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Queries
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    public class GetBookByIdQuery : IQuery<Result>
    {
        public GetBookByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}