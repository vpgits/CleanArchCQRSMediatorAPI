// <copyright file="UpdateStaffMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class UpdateStaffMemberCommand : ICommand
    {
        public string? Name { get; set; }

        public StaffMemberType StaffMemberType { get; set; }

        public Guid Id { get; set; }
    }
}