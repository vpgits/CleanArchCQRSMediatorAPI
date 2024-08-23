// <copyright file="CreateBookCommandHandlerTests.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

using AutoMapper;
using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
using CleanArchCQRSMediatorAPI.Application.Features.Books.Commands;
using CleanArchCQRSMediatorAPI.Application.Features.Books.Handlers;
using CleanArchCQRSMediatorAPI.Domain.Entities;
using FluentValidation;
using Moq;

namespace CleanArchCQRSMediatorAPI.Tests.Application.Features.Books
{
    public class CreateLibraryMemberCommandHandlerTests
    {
        private readonly CreateBookCommandHandler _handler;
        private readonly IMock<IGenericRepository<Book>> repository;
        private readonly IMock<IMapper> _mapper;
        private readonly IMock<IUnitOfWork> _unitOfWork;
        private readonly IMock<IValidator<CreateBookCommand>> _validator;

        public CreateLibraryMemberCommandHandlerTests()
        {
            repository = new Mock<IGenericRepository<Book>>();
            _mapper = new Mock<IMapper>();
            _validator = new Mock<IValidator<CreateBookCommand>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateBookCommandHandler(repository.Object, _mapper.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            var command = new CreateBookCommand { Author = "J. K. Rowling", BookCategory = BookCategory.FICTION, PublicationYear = 1998, Title = "Harry Potter 1" };
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }

    }
}