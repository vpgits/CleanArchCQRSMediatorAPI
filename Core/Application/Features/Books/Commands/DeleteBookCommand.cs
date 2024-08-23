// <copyright file="DeleteBookCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public record DeleteBookCommand : ICommand
    {
        public DeleteBookCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}