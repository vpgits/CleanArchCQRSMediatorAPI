// <copyright file="UserService.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Identity.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;
    using CleanArchCQRSMediatorAPI.Identity.Constants;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMediator mediator;
        private readonly IConfiguration configuration;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMediator mediator, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mediator = mediator;
            this.configuration = configuration;
        }

        public async Task<Result> LoginMember(string username, string password)
        {
            List<Error> errors =[];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(username))
            {
                return Result.Failure(new Error("400", "Username or Password is empty"));
            }

            var user = await this.userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Result.Failure(new Error("400", "User not found"));
            }

            if (!await this.userManager.CheckPasswordAsync(user, password!))
            {
                return Result.Failure(new Error("401", "Incorrect password"));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!) !,
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
            };

            var roles = await this.userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims = claims.Concat(new[] { new Claim(ClaimTypes.Role, role) }).ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"] !));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                       issuer: this.configuration["Jwt:Issuer"],
                       audience: this.configuration["Jwt:Audience"],
                       claims: claims,
                       expires: DateTime.Now.AddMinutes(30),
                       signingCredentials: creds);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Result.Success(new { Token = tokenString, Expiration = token.ValidTo });
        }

        public async Task<Result> RegisterLibraryMember(string username, string password)
        {
            var user = new IdentityUser { UserName = username };
            var result = await this.userManager.CreateAsync(user, password);
            var role = AuthorizationConstants.AuthorizationRoles.LibraryMember;
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    var roleExists = await this.roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        await this.roleManager.CreateAsync(new IdentityRole(role));
                    }

                    await this.userManager.AddToRoleAsync(user, role);
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(new Error("500", "Roles are empty"));
                }
            }
            else
            {
                return Result.Failure(new Error("400", string.Join(" ", result.Errors.Select(error => error.Description))));
            }
        }

        public async Task<Result> RegisterStaffMember(string username, string password, StaffMemberType staffRole)
        {
            var user = new IdentityUser { UserName = username };
            var result = await this.userManager.CreateAsync(user, password!);
            var role = staffRole == StaffMemberType.MANAGEMENT ? AuthorizationConstants.AuthorizationRoles.ManagementStaff : AuthorizationConstants.AuthorizationRoles.MinorStaff;
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    var roleExists = await this.roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        await this.roleManager.CreateAsync(new IdentityRole(role));
                    }

                    await this.userManager.AddToRoleAsync(user, role);
                }
            }

            return Result.Failure(new Error("400", string.Join(" ", result.Errors.Select(error => error.Description))));
        }
    }
}