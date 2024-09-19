// <copyright file="LibraryDbContext.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Context
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;
    using CleanArchCQRSMediatorAPI.Persistence.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<LibraryMember> LibraryMembers { get; set; }

        public DbSet<StaffMember> StaffMembers { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryMemberEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}