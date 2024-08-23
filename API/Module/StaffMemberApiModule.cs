// <copyright file="StaffMemberApiModule.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Module
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Queries;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Queries;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Identity.Constants;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    internal class StaffMemberApiModule : IApiModule
    {
        void IApiModule.MapEndpoint(WebApplication app)
        {
            var staffMembers = app.MapGroup("/staffMembers")/*.RequireAuthorization(AuthorizationConstants.AuthorizationPolicies.IsStaff)*/;
            staffMembers.MapGet("/{id}", GetStaffMemberById);

            // staffMembers.MapPost("/", CreateStaffMember);
            staffMembers.MapGet("/", GetAllStaffMembers)/*.RequireAuthorization(AuthorizationConstants.AuthorizationPolicies.ManagementStaffOnly)*/;
            staffMembers.MapPut("/{id}", UpdateStaffMember);
            staffMembers.MapDelete("/{id}", DeleteStaffMember);
            staffMembers.MapGet("/allMembers", GetAllLibraryMembers);
        }

        private static async Task<Result> CreateStaffMember(CreateStaffMemberCommand command, [FromServices] IMediator mediator)
        {
            return await mediator.Send(command);
        }

        private static async Task<Result> GetStaffMemberById(Guid id, [FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetStaffMemberByIdQuery(id));
        }

        private static async Task<Result> GetAllStaffMembers([FromServices] IMediator mediator)
        {
            return Result.Success(await mediator.Send(new GetAllStaffMembersQuery()));
        }

        private static async Task<Result> UpdateStaffMember(Guid id, UpdateStaffMemberCommand command, [FromServices] IMediator mediator)
        {
            if (command.Id != id)
            {
                return Result.Failure(new Error("400", "Id mismatch between payload and URL Path Parameter"));
            }

            return await mediator.Send(command);
        }

        private static async Task<Result> DeleteStaffMember(Guid id, [FromServices] IMediator mediator)
        {
            return await mediator.Send(new DeleteStaffMemberCommand(id));
        }

        private static async Task<Result> GetAllLibraryMembers([FromServices] IMediator mediator)
        {
            return Result.Success(await mediator.Send(new GetAllLibraryMembersQuery()));
        }

        private static async Task<Result> GetAllBooks(Guid id, [FromServices] IMediator mediator)
        {
            var isManagement = await mediator.Send(new IsManagementStaffQuery(id));
            if (!isManagement)
            {
                throw new UnauthorizedAccessException();
            }

            return await mediator.Send(new GetAllBooksDtoQuery());
        }
    }
}