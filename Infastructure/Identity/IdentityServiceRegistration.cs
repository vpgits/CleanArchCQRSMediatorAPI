// <copyright file="IdentityServiceRegistration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Identity
{
    using System.Security.Claims;
    using System.Text;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Identity.Contexts;
    using CleanArchCQRSMediatorAPI.Identity.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class IdentityServiceRegistration
    {
        public static IServiceCollection RegisterIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<AuthDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserService, UserService>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"] !,
                    ValidAudience = configuration["Jwt:Audience"] !,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] !)),
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("management-staff-only", policy =>
                {
                    policy.RequireRole("management-staff");
                });
                options.AddPolicy("minor-staff-only", policy =>
                {
                    policy.RequireRole("minor-staff");
                });
                options.AddPolicy("library-member-only", policy =>
                {
                    policy.RequireRole("library-member");
                });
                options.AddPolicy("get-all-books", policy =>
                {
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(claim => claim.Type == ClaimTypes.Role && (claim.Value == "management-staff" || claim.Value == "library-member")));
                });
                options.AddPolicy("is-staff", policy =>
                {
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(claim => claim.Type == ClaimTypes.Role && (claim.Value == "management-staff" || claim.Value == "minor-staff")));
                });
            });
            return services;
        }
    }
}