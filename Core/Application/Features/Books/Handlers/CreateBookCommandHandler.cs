// <copyright file="CreateBookCommandHandler.cs" company="vpgits">
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

    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IGenericRepository<Book> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateBookCommandHandler(IGenericRepository<Book> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = this.mapper.Map<Book>(command);
            var savedBook = await this.repository.Add(book);
            var task = this.unitOfWork.SaveChangesAsync();
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var bookDto = this.mapper.Map<BookDto>(savedBook);
                bookDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(savedBook);
                bookDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(savedBook);
                return Result.Success(bookDto);
            }
            else
            {
                return Result.Failure(new Error("400", "Failed to add a book"));
            }
        }
    }
}