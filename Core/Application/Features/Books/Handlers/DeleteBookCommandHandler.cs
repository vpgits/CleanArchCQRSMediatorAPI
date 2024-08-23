// <copyright file="DeleteBookCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly IGenericRepository<Book> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public DeleteBookCommandHandler(IGenericRepository<Book> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var book = await this.repository.GetByIdAsync(command.Id);
            if (book == null)
            {
                return Result.Failure(new Error("404", $"Book with Guid {command.Id} not found"));
            }

            this.repository.Delete(book);
            var task = this.unitOfWork.SaveChangesAsync(cancellationToken);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var bookDto = this.mapper.Map<BookDto>(book);
                bookDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(book);
                bookDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(book);
                return Result.Success(bookDto);
            }

            return Result.Failure(new Error("500", "Unable to remove the book"));
        }
    }
}