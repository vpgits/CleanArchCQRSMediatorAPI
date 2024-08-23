// <copyright file="ReturnBookLibraryMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public class ReturnBookLibraryMemberCommand : ICommand
    {
        public Guid BookId { get; set; }

        public Guid MemberId { get; set; }
    }
}