// <copyright file="GetAllBooksQueryHandler.cs" company="vpgits">
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

    public class GetAllBooksQueryHandler : IQueryHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IGenericRepository<Book> repository;
        private readonly IMapper mapper;

        public GetAllBooksQueryHandler(IGenericRepository<Book> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await this.repository.GetAllAsync();

            var bookDtos = books.Select(book =>
            {
                var bookDto = this.mapper.Map<BookDto>(book);
                bookDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(book);
                bookDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(book);
                return bookDto;
            }).ToList();

            return (List<BookDto>)bookDtos;
        }
    }
}