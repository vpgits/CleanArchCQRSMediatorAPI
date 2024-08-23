// <copyright file="RedisCacheService.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Utility
{
    using System.Text.Json;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Utility;
    using Microsoft.Extensions.Caching.Distributed;

    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache cache;

        public RedisCacheService(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var cachedData = await this.GetCachedDataAsync(key, cancellationToken);

            if (cachedData != null)
            {
                return this.Deserialize<T>(cachedData);
            }

            var result = await factory(cancellationToken);
            await this.CacheDataAsync(key, result, expiration, cancellationToken);
            return result;
        }

        private async Task<string?> GetCachedDataAsync(string key, CancellationToken cancellationToken)
        {
            return await this.cache.GetStringAsync(key, cancellationToken);
        }

        private async Task CacheDataAsync<T>(string key, T data, TimeSpan? expiration, CancellationToken cancellationToken)
        {
            var jsonData = this.Serialize(data);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(1),
            };
            await this.cache.SetStringAsync(key, jsonData, options, cancellationToken);
        }

        private string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data);
        }

        private T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data) !;
        }
    }
}