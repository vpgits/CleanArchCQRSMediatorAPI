// <copyright file="BookApiModule.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Module
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Commands;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Identity.Constants;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    internal class BookApiModule : IApiModule
    {
        void IApiModule.MapEndpoint(WebApplication app)
        {
            var books = app.MapGroup("/books")/*.RequireAuthorization(AuthorizationConstants.AuthorizationPolicies.ManagementStaffOnly)*/;
            app.MapGet("/books", GetAllBooks)/*.RequireAuthorization(AuthorizationConstants.AuthorizationPolicies.GetAllBooks)*/;
            books.MapPost("/", AddBook);
            books.MapDelete("/", RemoveBook);
            books.MapGet("/{id}", GetBookById);
            books.MapPut("/", UpdateBook);
        }

        private static async Task<Result> GetAllBooks([FromServices] IMediator mediator)
        {
            return Result.Success(await mediator.Send(new GetAllBooksQuery()));
        }

        private static async Task<Result> AddBook(CreateBookCommand command, [FromServices] IMediator mediator)
        {
            return await mediator.Send(command);
        }

        private static async Task<Result> RemoveBook(Guid id, [FromServices] IMediator mediator)
        {
            return await mediator.Send(new DeleteBookCommand(id));
        }

        private static async Task<Result> GetBookById(Guid id, [FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetBookByIdQuery(id));
        }

        private static async Task<Result> UpdateBook(UpdateBookCommand command, [FromServices] IMediator mediator)
        {
            // if (id != command.Id)
            // {
            //    return Result.Failure(new Error("400", "Id mismatch between payload and URL Path Parameter"));
            // }
            return await mediator.Send(command);
        }
    }
}