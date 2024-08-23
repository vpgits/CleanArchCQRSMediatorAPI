// <copyright file="ApplicationServiceRegistration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application
{
    using CleanArchCQRSMediatorAPI.Application.Behaviours;
    using CleanArchCQRSMediatorAPI.Application.Profiles;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookProfiles), typeof(MemberProfiles), typeof(StaffMemberProfiles), typeof(LibraryMemberProfiles));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
            return services;
        }
    }
}