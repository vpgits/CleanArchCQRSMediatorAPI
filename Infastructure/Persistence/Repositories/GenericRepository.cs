// <copyright file="GenericRepository.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Persistence.Context;
    using Microsoft.EntityFrameworkCore;

    internal class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly LibraryDbContext dbContext;
        private DbSet<TEntity> entities;

        public GenericRepository(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();
        }

        public Task<TEntity> Add(TEntity entity)
        {
            this.dbContext.Add(entity);
            return Task.FromResult(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            this.dbContext.Remove(entity);
            return entity;
        }

        public async Task<IReadOnlyList<TEntity?>> GetAllAsync()
        {
            return await this.entities.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            PropertyInfo idProperty = typeof(TEntity).GetProperty("Id") !;
            return await this.entities.Where(e => Equals(idProperty.GetValue(e), id)).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public TEntity Update(TEntity entity)
        {
            this.dbContext.Update(entity);
            return entity;
        }

        public DateTime GetCreatedAtShadowProperty(TEntity entity)
        {
            return this.dbContext.Entry(entity).Property<DateTime>("CreatedAt").CurrentValue;
        }

        public DateTime GetUpdatedAtShadowProperty(TEntity entity)
        {
            return this.dbContext.Entry(entity).Property<DateTime>("UpdatedAt").CurrentValue;
        }
    }
}