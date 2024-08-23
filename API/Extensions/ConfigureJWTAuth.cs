// <copyright file="ConfigureJWTAuth.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Extensions
{
    using System.Security.Claims;
    using System.Text;
    using CleanArchCQRSMediatorAPI.Identity.Constants;
    using CleanArchCQRSMediatorAPI.Identity.Contexts;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    public static class ConfigureJWTAuth
    {
        public static IServiceCollection RegisterJWTAuth(this IServiceCollection services, IConfiguration configuration)
        {
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
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"] !,
                    ValidAudience = configuration["Jwt:Audience"] !,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] !)),
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConstants.AuthorizationPolicies.ManagementStaffOnly, policy =>
                {
                    policy.RequireRole(AuthorizationConstants.AuthorizationRoles.ManagementStaff);
                });

                options.AddPolicy(AuthorizationConstants.AuthorizationPolicies.MinorStaffOnly, policy =>
                {
                    policy.RequireRole(AuthorizationConstants.AuthorizationRoles.MinorStaff);
                });

                options.AddPolicy(AuthorizationConstants.AuthorizationPolicies.LibraryMemberOnly, policy =>
                {
                    policy.RequireRole(AuthorizationConstants.AuthorizationRoles.LibraryMember);
                });

                options.AddPolicy(AuthorizationConstants.AuthorizationPolicies.GetAllBooks, policy =>
                {
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(claim => claim.Type == ClaimTypes.Role &&
                        (claim.Value == AuthorizationConstants.AuthorizationRoles.ManagementStaff ||
                         claim.Value == AuthorizationConstants.AuthorizationRoles.LibraryMember)));
                });

                options.AddPolicy(AuthorizationConstants.AuthorizationPolicies.IsStaff, policy =>
                {
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(claim => claim.Type == ClaimTypes.Role &&
                        (claim.Value == AuthorizationConstants.AuthorizationRoles.ManagementStaff ||
                         claim.Value == AuthorizationConstants.AuthorizationRoles.MinorStaff)));
                });
            });
            return services;
        }
    }
}