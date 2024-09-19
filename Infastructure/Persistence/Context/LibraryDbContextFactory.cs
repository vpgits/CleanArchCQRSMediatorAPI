// <copyright file="LibraryDbContextFactory.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            // Find the API project directory
            string currentDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = FindSolutionDirectory(currentDirectory);
            string apiProjectDirectory = Path.Combine(solutionDirectory, "API");

            // Ensure the API project directory exists
            if (!Directory.Exists(apiProjectDirectory))
            {
                throw new DirectoryNotFoundException($"API project directory not found at {apiProjectDirectory}");
            }

            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

            // Create DbContextOptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();

            // Get connection string from configuration
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure DbContext
            optionsBuilder.UseNpgsql(connectionString, b =>
                b.MigrationsAssembly(typeof(LibraryDbContext).Assembly.GetName().Name));

            // Enable lazy loading
            optionsBuilder.UseLazyLoadingProxies();

            // Create and return DbContext instance
            return new LibraryDbContext(optionsBuilder.Options);
        }

        private string FindSolutionDirectory(string startDirectory)
        {
            while (startDirectory != null)
            {
                if (Directory.GetFiles(startDirectory, "*.sln").Length > 0)
                {
                    return startDirectory;
                }
                startDirectory = Directory.GetParent(startDirectory)?.FullName;
            }
            throw new DirectoryNotFoundException("Solution directory not found.");
        }
    }
}