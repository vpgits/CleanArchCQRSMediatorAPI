// <copyright file="ICachedQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging
{
    public interface ICachedQuery
    {
        string Key { get; }

        public TimeSpan? Expiration { get; }
    }

    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery
    {
    }
}