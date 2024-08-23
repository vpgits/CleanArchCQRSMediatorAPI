// <copyright file="IsManagementStaffQueryHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Handlers
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class IsManagementStaffQueryHandler : IQueryHandler<IsManagementStaffQuery, bool>
    {
        private readonly IGenericRepository<StaffMember> repository;

        public IsManagementStaffQueryHandler(IGenericRepository<StaffMember> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(IsManagementStaffQuery request, CancellationToken cancellationToken)
        {
            return (await this.repository.GetByIdAsync(request.Id)) !.StaffType == StaffMemberType.MANAGEMENT;
        }
    }
}