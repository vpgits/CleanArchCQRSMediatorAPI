// <copyright file="StaffMemberRegisterRequest.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class StaffMemberRegisterRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public StaffMemberType? Role { get; set; } = 0;
    }
}