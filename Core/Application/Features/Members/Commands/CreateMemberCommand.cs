// <copyright file="CreateMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Members.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class CreateMemberCommand : ICommand
    {
        public string? Name { get; set; }

        public MemberType MemberType { get; set; }
    }
}