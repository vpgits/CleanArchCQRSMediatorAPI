// <copyright file="GetAllStaffMembersQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Queries
{
    using System;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    public record GetAllStaffMembersQuery : ICachedQuery<List<StaffMemberDto>>
    {
        public string Key => "$get-all-staff";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
    }
}