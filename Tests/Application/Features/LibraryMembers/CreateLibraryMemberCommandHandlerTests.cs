// <copyright file="CreateLibraryMemberCommandHandlerTests.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

using AutoMapper;
using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Handlers;
using CleanArchCQRSMediatorAPI.Domain.Entities;
using FluentValidation;
using Moq;

namespace CleanArchCQRSMediatorAPI.Tests.Application.Features.LibraryMembers
{
    public class CreateLibraryMemberCommandHandlerTests
    {
        private readonly CreateLibraryMemberCommandHandler _handler;
        private readonly IMock<IGenericRepository<LibraryMember>> repository;
        private readonly IMock<IMapper> _mapper;
        private readonly IMock<IUnitOfWork> _unitOfWork;
        private readonly IMock<IValidator<CreateLibraryMemberCommand>> _validator;
        private readonly IMock<IUserService> _userService;

        public CreateLibraryMemberCommandHandlerTests()
        {
            repository = new Mock<IGenericRepository<LibraryMember>>();
            _mapper = new Mock<IMapper>();
            _validator = new Mock<IValidator<CreateLibraryMemberCommand>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _userService = new Mock<IUserService>();
            _handler = new CreateLibraryMemberCommandHandler(repository.Object, _mapper.Object, _unitOfWork.Object, _userService.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            var command = new CreateLibraryMemberCommand { Username = "Test User" };
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }

    }
}