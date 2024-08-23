// <copyright file="MemberDto.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Dtos
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class MemberDto
    {
        public MemberDto()
        {
        }

        public string? Username { get; set; }

        public MemberType MemberType { get; set; }
    }
}