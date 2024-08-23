// <copyright file="LoginMemberCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Common.Handlers
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Features.Common.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    internal class LoginMemberCommandHandler : ICommandHandler<LoginMemberCommand>
    {
        private readonly IUserService userService;

        public LoginMemberCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Result> Handle(LoginMemberCommand command, CancellationToken cancellationToken)
        {
            return await this.userService.LoginMember(command.Username!, command.Password!);
        }
    }
}