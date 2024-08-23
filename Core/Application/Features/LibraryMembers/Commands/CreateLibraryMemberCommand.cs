// <copyright file="CreateLibraryMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public record CreateLibraryMemberCommand : ICommand
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}