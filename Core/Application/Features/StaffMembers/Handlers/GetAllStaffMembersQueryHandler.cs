// <copyright file="GetAllStaffMembersQueryHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class GetAllStaffMembersQueryHandler : IQueryHandler<GetAllStaffMembersQuery, List<StaffMemberDto>>
    {
        private readonly IGenericRepository<StaffMember> repository;
        private readonly IMapper mapper;

        public GetAllStaffMembersQueryHandler(IGenericRepository<StaffMember> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<StaffMemberDto>> Handle(GetAllStaffMembersQuery request, CancellationToken cancellationToken)
        {
            var staffMembers = await this.repository.GetAllAsync();

            var staffMemberDtos = staffMembers.Select(staffMember =>
            {
                var staffMemberDto = this.mapper.Map<StaffMemberDto>(staffMember);
                staffMemberDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(staffMember);
                staffMemberDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(staffMember);
                return staffMemberDto;
            }).ToList();

            return (List<StaffMemberDto>)staffMemberDtos;
        }
    }
}