// <copyright file="IGenericRepository.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id);

        Task<IReadOnlyList<TEntity?>> GetAllAsync();

        Task<TEntity> Add(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        DateTime GetCreatedAtShadowProperty(TEntity entity);

        DateTime GetUpdatedAtShadowProperty(TEntity entity);
    }
}