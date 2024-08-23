// <copyright file="GetBookByIdQueryHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, Result>
    {
        private readonly IGenericRepository<Book> repository;
        private readonly IMapper mapper;

        public GetBookByIdQueryHandler(IGenericRepository<Book> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await this.repository.GetByIdAsync(request.Id);

            if (book == null)
            {
                return Result.Failure(new Error("404", "Book not found"));
            }

            var bookDto = this.mapper.Map<BookDto>(book);
            bookDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(book);
            bookDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(book);

            return Result.Success(bookDto);
        }
    }
}