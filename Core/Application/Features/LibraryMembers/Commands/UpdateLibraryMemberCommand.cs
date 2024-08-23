// <copyright file="UpdateLibraryMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class UpdateLibraryMemberCommand : ICommand
    {
        public string? Name { get; set; }

        public List<Book>? Books { get; set; }

        public Guid Id { get; set; }
    }
}