// <copyright file="UpdateBookCommandHandler.cs" company="vpgits">
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

    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
    {
        private readonly IGenericRepository<Book> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateBookCommandHandler(IGenericRepository<Book> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await this.repository.GetByIdAsync(command.Id);
            if (book == null)
            {
                return Result.Failure(new Error("404", $"Book not found for Guid {command.Id}"));
            }

            var newBook = this.mapper.Map<Book>(command);
            this.repository.Update(newBook);
            var task = this.unitOfWork.SaveChangesAsync(cancellationToken);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var bookDto = this.mapper.Map<BookDto>(newBook);
                bookDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(newBook);
                bookDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(newBook);
                return Result.Success(bookDto);
            }

            return Result.Failure(new Error("500", "Unable to update the book"));
        }
    }
}