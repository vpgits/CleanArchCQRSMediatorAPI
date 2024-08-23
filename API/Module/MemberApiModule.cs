// <copyright file="MemberApiModule.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Module
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;
    using CleanArchCQRSMediatorAPI.Application.Features.Members.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    internal class MemberApiModule : IApiModule
    {
        void IApiModule.MapEndpoint(WebApplication app)
        {
            var members = app.MapGroup("/members");
            members.MapGet("/", GetAllMembers);
        }

        private static async Task<Result> GetAllMembers([FromServices] IMediator mediator)
        {
            return Result.Success(await mediator.Send(new GetAllMembersQuery()));
        }
    }
}