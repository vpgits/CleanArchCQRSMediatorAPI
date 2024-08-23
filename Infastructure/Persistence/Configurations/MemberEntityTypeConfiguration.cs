// <copyright file="MemberEntityTypeConfiguration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Configurations
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Member");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Username).IsRequired().HasMaxLength(256);
            builder.Property(m => m.MemberType).IsRequired();
            builder.Property<DateTime>("CreatedAt");
            builder.Property<DateTime>("UpdatedAt");
        }
    }
}