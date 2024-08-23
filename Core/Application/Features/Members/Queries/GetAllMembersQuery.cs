// <copyright file="GetAllMembersQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Members.Queries
{
    using System;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    public record GetAllMembersQuery : ICachedQuery<List<MemberDto>>
    {
        public string Key => "$get-all-members";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
    }
}