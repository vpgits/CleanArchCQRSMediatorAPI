// <copyright file="LibraryMemberEntityTypeConfiguration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Configurations
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class LibraryMemberEntityTypeConfiguration : IEntityTypeConfiguration<LibraryMember>
    {
        public void Configure(EntityTypeBuilder<LibraryMember> builder)
        {
            builder.ToTable("Member");
            builder.HasBaseType<Member>();
            builder.Property(m => m.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.HasMany(m => m.BorrowedBooks).WithOne(b => b.BorrowedMember).HasForeignKey(b => b.BorrowedMemberId).OnDelete(DeleteBehavior.Cascade);
            builder.Property<DateTime>("CreatedAt").HasDefaultValueSql("NOW()");
            builder.Property<DateTime>("UpdatedAt").HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
        }
    }
}