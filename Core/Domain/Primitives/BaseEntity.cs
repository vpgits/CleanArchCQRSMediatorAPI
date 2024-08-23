// <copyright file="BaseEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Domain.Primitives
{
    public class BaseEntity
    {
        public Guid Id { get; private init; }
    }
}