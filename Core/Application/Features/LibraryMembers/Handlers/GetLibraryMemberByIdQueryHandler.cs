// <copyright file="GetLibraryMemberByIdQueryHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class GetLibraryMemberByIdQueryHandler : IQueryHandler<GetLibraryMemberByIdQuery, Result>
    {
        private readonly IGenericRepository<LibraryMember> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetLibraryMemberByIdQueryHandler(IGenericRepository<LibraryMember> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetLibraryMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var libraryMember = await this.repository.GetByIdAsync(request.Id);
            if (libraryMember == null)
            {
                return Result.Failure(new Error("400", $"Unable to find a Library user with User Id {request.Id}"));
            }

            var libraryMemberDto = this.mapper.Map<LibraryMemberDto>(libraryMember);
            libraryMemberDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(libraryMember);
            libraryMemberDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(libraryMember);
            return Result.Success(libraryMemberDto);
        }
    }
}