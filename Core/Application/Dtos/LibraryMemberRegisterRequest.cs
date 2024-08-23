// <copyright file="LibraryMemberRegisterRequest.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class LibraryMemberRegisterRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}