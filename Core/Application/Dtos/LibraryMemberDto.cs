// <copyright file="LibraryMemberDto.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Dtos
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class LibraryMemberDto
    {
        public LibraryMemberDto()
        {
        }

        public Guid Id { get; set; }

        public string? Username { get; set; }

        public MemberType MemberType { get; set; }

        public List<string> BorrowedBookIds { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}