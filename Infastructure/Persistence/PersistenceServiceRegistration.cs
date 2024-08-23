// <copyright file="PersistenceServiceRegistration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Persistence.Context;
    using CleanArchCQRSMediatorAPI.Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork<LibraryDbContext>>();
            return services;
        }
    }
}