// <copyright file="IsManagementStaffQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;

    public class IsManagementStaffQuery : IQuery<bool>
    {
        public IsManagementStaffQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}