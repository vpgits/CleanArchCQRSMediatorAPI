// <copyright file="DeleteLibraryMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public record DeleteLibraryMemberCommand : ICommand
    {
        public DeleteLibraryMemberCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}