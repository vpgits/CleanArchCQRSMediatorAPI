// <copyright file="MigrateAuthExtensions.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Identity.Extensions
{
    using CleanArchCQRSMediatorAPI.Identity.Contexts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class MigrateAuthExtensions
    {
        public static IHost MigrateAuthDbContext(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AuthDbContext>();
                dbContext.Database.Migrate();
            }

            return host;
        }
    }
}