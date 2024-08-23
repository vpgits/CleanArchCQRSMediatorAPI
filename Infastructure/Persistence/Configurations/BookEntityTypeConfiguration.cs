// <copyright file="BookEntityTypeConfiguration.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Configurations
{
    using CleanArchCQRSMediatorAPI.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(book => book.Id);
            builder.Property(b => b.BorrowedMemberId).IsRequired(false);
            builder.Ignore(b => b.BorrowedMember);
            builder.HasOne(b => b.BorrowedMember).WithMany(m => m.BorrowedBooks).HasForeignKey(b => b.BorrowedMemberId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(b => b.Author).HasMaxLength(256).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(256).IsRequired();
            builder.Property(b => b.PublicationYear).IsRequired();
            builder.Property(b => b.IsAvailable).HasDefaultValue(true);
            builder.Property<DateTime>("CreatedAt");
            builder.Property<DateTime>("UpdatedAt");
        }
    }
}