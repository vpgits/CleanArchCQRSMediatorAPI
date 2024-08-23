// <copyright file="DeleteStaffMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public class DeleteStaffMemberCommand : ICommand
    {
        public DeleteStaffMemberCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}