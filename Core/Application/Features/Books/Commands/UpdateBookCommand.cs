// <copyright file="UpdateBookCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class UpdateBookCommand : ICommand
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public int PublicationYear { get; set; }

        public BookCategory BookCategory { get; set; }

        public bool IsAvailable { get; set; }

        public Guid? BorrowedMemberId { get; set; }
    }
}