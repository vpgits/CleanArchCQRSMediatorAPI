// <copyright file="UnitOfWork.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Domain.Primitives;

    using Microsoft.EntityFrameworkCore;

    internal class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = dbContext.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }

                entity.UpdatedAt = DateTime.UtcNow;
            }
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}