// <copyright file="GetAllLibraryMembersQueryHandler.cs" company="vpgits">
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

    public class GetAllLibraryMembersQueryHandler : IQueryHandler<GetAllLibraryMembersQuery, List<LibraryMemberDto>>
    {
        private readonly IGenericRepository<LibraryMember> repository;
        private readonly IMapper mapper;

        public GetAllLibraryMembersQueryHandler(IGenericRepository<LibraryMember> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<LibraryMemberDto>> Handle(GetAllLibraryMembersQuery request, CancellationToken cancellationToken)
        {
            var libraryMembers = await this.repository.GetAllAsync();

            var libraryMemberDtos = libraryMembers.Select(libraryMember =>
            {
                var libraryMemberDto = this.mapper.Map<LibraryMemberDto>(libraryMember);
                libraryMemberDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(libraryMember);
                libraryMemberDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(libraryMember);
                return libraryMemberDto;
            }).ToList();

            return (List<LibraryMemberDto>)libraryMemberDtos;
        }
    }
}