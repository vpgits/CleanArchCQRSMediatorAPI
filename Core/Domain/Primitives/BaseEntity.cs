// <copyright file="BaseEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchCQRSMediatorAPI.Domain.Primitives
{
    public class BaseEntity
    {
        public Guid Id { get; private init; }

        [NotMapped]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public DateTime UpdatedAt { get; set; }

    }
}