// <copyright file="UtilityServiceRegistration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Utility
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Utility;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class UtilityServiceRegistration
    {
        public static IServiceCollection ConfigureUtilityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            // enable redis cache
            // services.AddStackExchangeRedisCache(opt =>
            // {
            //    opt.Configuration = configuration.GetConnectionString("Redis");
            //    opt.InstanceName = "CleanArchCQRSMediatorAPI";
            // });
            // services.AddSingleton<ICacheService, RedisCacheService>();
            return services;
        }
    }
}