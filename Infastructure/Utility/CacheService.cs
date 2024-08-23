// <copyright file="CacheService.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Utility
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Utility;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using Microsoft.Extensions.Caching.Memory;

    public class CacheService : ICacheService
    {
        private static readonly TimeSpan DefaultExpirationTime = TimeSpan.FromMinutes(1);
        private readonly IMemoryCache memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            T result = await this.memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpirationTime);
                return factory(cancellationToken);
            });
            return result;
        }
    }
}