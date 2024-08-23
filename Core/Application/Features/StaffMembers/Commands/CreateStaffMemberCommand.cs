// <copyright file="CreateStaffMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class CreateStaffMemberCommand : ICommand
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public StaffMemberType? Role { get; set; } = 0;
    }
}