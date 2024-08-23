// <copyright file="CreateBookCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class CreateBookCommand : ICommand
    {
        public string? Title { get; set; }

        public string? Author { get; set; }

        public int PublicationYear { get; set; }

        public BookCategory BookCategory { get; set; }
    }
}