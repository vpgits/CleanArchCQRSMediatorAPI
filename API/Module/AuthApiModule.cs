// <copyright file="AuthApiModule.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Module
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.Common.Commands;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using MediatR;

    public class AuthApiModule : IApiModule
    {
        public void MapEndpoint(WebApplication app)
        {
            var auth = app.MapGroup("/auth");
            var staff = auth.MapGroup("/staff");
            staff.MapPost("/register", RegisterStaffMember);
            staff.MapPost("/login", LoginUser);
            var library = auth.MapGroup("/library");
            library.MapPost("/register", RegisterLibraryMember);
            library.MapPost("/login", LoginUser);
        }

        private static async Task<Result> RegisterStaffMember(CreateStaffMemberCommand command, IMediator mediator)
        {
            return await mediator.Send(command);
        }

        private static async Task<Result> RegisterLibraryMember(CreateLibraryMemberCommand command, IMediator mediator)
        {
            return await mediator.Send(command);
        }

        private static async Task<Result> LoginUser(LoginMemberCommand command, IMediator mediator)
        {
            return await mediator.Send(command);
        }
    }
}