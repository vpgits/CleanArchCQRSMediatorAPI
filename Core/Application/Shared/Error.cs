// <copyright file="Error.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Shared
{
    public sealed record Error(string code, string? description = null)
    {
        public static readonly Error None = new (string.Empty);
    }
}