// <copyright file="GetStaffMemberByIdQueryHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class GetStaffMemberByIdQueryHandler : IQueryHandler<GetStaffMemberByIdQuery, Result>
    {
        private readonly IGenericRepository<StaffMember> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetStaffMemberByIdQueryHandler(IGenericRepository<StaffMember> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetStaffMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var staffMember = await this.repository.GetByIdAsync(request.Id);
            if (staffMember == null)
            {
                return Result.Failure(new Error("400", $"Unable to find a Library user with User Id {request.Id}"));
            }

            var staffMemberDto = this.mapper.Map<StaffMemberDto>(staffMember);
            staffMemberDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(staffMember);
            staffMemberDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(staffMember);
            return Result.Success(staffMemberDto);
        }
    }
}