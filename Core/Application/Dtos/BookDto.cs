// <copyright file="BookDto.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Dtos
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class BookDto
    {
        public BookDto()
        {
        }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public bool IsAvailable { get; set; }

        public int PublicationYear { get; set; }

        public BookCategory BookCategory { get; set; }

        public Guid? BorrowedMemberId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}