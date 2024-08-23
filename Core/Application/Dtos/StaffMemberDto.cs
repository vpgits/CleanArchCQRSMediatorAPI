// <copyright file="StaffMemberDto.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Dtos
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class StaffMemberDto
    {
        public StaffMemberDto()
        {
        }

        public Guid Id { get; set; }

        public string? Username { get; set; }

        public MemberType MemberType { get; set; }

        public StaffMemberType StaffType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}