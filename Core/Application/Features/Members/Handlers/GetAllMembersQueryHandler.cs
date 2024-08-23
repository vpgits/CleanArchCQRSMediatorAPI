// <copyright file="GetAllMembersQueryHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Members.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.Members.Queries;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class GetAllMembersQueryHandler : IQueryHandler<GetAllMembersQuery, List<MemberDto>>
    {
        private readonly IGenericRepository<Member> repository;
        private readonly IMapper mapper;

        public GetAllMembersQueryHandler(IGenericRepository<Member> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            return (await this.repository.GetAllAsync() ?? new List<Member>()).Select(m => this.mapper.Map<MemberDto>(m)).ToList();
        }
    }
}