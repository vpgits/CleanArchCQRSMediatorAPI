// <copyright file="GetAllBooksQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Queries
{
    using System;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Dtos;

    public record GetAllBooksQuery : ICachedQuery<List<BookDto>>
    {
        public string Key => "$get-all-books";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
    }
}