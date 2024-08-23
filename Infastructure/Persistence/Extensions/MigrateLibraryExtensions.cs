// <copyright file="MigrateLibraryExtensions.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Extensions
{
    using CleanArchCQRSMediatorAPI.Persistence.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class MigrateLibraryExtensions
    {
        public static IHost MigrateLibraryDbContext(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<LibraryDbContext>();
                dbContext.Database.Migrate();
            }

            return host;
        }
    }
}