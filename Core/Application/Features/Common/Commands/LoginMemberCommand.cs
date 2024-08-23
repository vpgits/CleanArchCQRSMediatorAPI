// <copyright file="LoginMemberCommand.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Common.Commands
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public class LoginMemberCommand : ICommand
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}