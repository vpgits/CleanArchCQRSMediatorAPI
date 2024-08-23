// <copyright file="IUserService.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Abstractions.Identity
{
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public interface IUserService
    {
        public Task<Result> RegisterStaffMember(string username, string password, StaffMemberType staffMemberType);

        public Task<Result> RegisterLibraryMember(string username, string password);

        public Task<Result> LoginMember(string username, string password);
    }
}