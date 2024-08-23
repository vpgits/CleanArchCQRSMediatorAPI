// <copyright file="LibraryMemberApiModule.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Module
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Queries;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Queries;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    internal class LibraryMemberApiModule : IApiModule
    {
        void IApiModule.MapEndpoint(WebApplication app)
        {
            var libraryMembers = app.MapGroup("/libraryMembers").RequireAuthorization("library-member-only");
            libraryMembers.MapGet("/{id}", GetLibraryMemberById);

            // libraryMembers.MapPost("/", CreateLibraryMember);
            libraryMembers.MapGet("/", GetAllLibraryMembers);
            libraryMembers.MapPut("/{id}", UpdateLibraryMember);
            libraryMembers.MapDelete("/{id}", DeleteLibraryMember);
            libraryMembers.MapPost("/burrow", BorrowBookLibraryMember);
            libraryMembers.MapPost("/return", ReturnBookLibraryMemberCommand);
            libraryMembers.MapGet("/{id}/allBooks", GetAllBooks);
        }

        private static async Task<Result> CreateLibraryMember(CreateLibraryMemberCommand command, [FromServices] IMediator mediator)
        {
            return await mediator.Send(command);
        }

        private static async Task<Result> GetLibraryMemberById(Guid id, [FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetLibraryMemberByIdQuery(id));
        }

        private static async Task<Result> GetAllLibraryMembers([FromServices] IMediator mediator)
        {
            return Result.Success(await mediator.Send(new GetAllLibraryMembersQuery()));
        }

        private static async Task<Result> UpdateLibraryMember(Guid id, UpdateLibraryMemberCommand command, [FromServices] IMediator mediator)
        {
            if (command.Id != id)
            {
                return Result.Failure(new Error("400", "Id mismatch between payload and URL Path Parameter"));
            }

            return await mediator.Send(command);
        }

        private static async Task<Result> DeleteLibraryMember(Guid id, [FromServices] IMediator mediator)
        {
            return await mediator.Send(new DeleteLibraryMemberCommand(id));
        }

        private static async Task<Result> BorrowBookLibraryMember(BorrowBookLibrayMemberCommand command, [FromServices] IMediator mediator)
        {
            return await mediator.Send(command);
        }

        private static async Task<Result> ReturnBookLibraryMemberCommand(ReturnBookLibraryMemberCommand command, [FromServices] IMediator mediator)
        {
            return await mediator.Send(command);
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