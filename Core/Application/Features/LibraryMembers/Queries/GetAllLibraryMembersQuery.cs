// <copyright file="GetAllLibraryMembersQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Queries
{
    using System;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    public record GetAllLibraryMembersQuery : ICachedQuery<List<LibraryMemberDto>>
    {
        public string Key => "$get-all-library";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
    }
}