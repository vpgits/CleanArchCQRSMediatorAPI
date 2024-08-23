// <copyright file="GetAllLibraryMembersQueryHandlerTests.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

using AutoMapper;
using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
using CleanArchCQRSMediatorAPI.Application.Dtos;
using CleanArchCQRSMediatorAPI.Application.Features.Books.Commands;
using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Handlers;
using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Queries;
using CleanArchCQRSMediatorAPI.Domain.Entities;
using FluentValidation;
using Moq;

namespace CleanArchCQRSMediatorAPI.Tests.Application.Features.LibraryMembers
{
    public class GetAllLibraryMembersQueryHandlerTests
    {
        private readonly GetAllLibraryMembersQueryHandler _handler;
        private readonly IMock<IGenericRepository<LibraryMember>> repository;
        private readonly IMock<IMapper> _mapper;
        public GetAllLibraryMembersQueryHandlerTests()
        {
            repository = new Mock<IGenericRepository<LibraryMember>>();
            _mapper = new Mock<IMapper>();
            _handler = new GetAllLibraryMembersQueryHandler(repository.Object, _mapper.Object);
        }
        [Fact]
        public async Task Handler_ShouldReturnSuccess()
        {
            var query = new GetAllLibraryMembersQuery();
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.IsType<List<LibraryMemberDto>>(result);
        }

    }
}