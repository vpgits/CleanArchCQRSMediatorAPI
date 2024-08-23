// <copyright file="CachingBehaviour.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Behaviours
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Utility;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using MediatR;

    public class CachingBehaviour<TRequest, TResponse, TIQuery>
           where TRequest : ICachedQuery<TIQuery>
           where TIQuery : IQuery<Result>
           where TResponse : Result
    {
        private readonly ICacheService cacheService;

        public CachingBehaviour(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            return await this.cacheService.GetOrCreateAsync(
                request.Key,
                _ => next(),
                request.Expiration,
                cancellationToken);
        }
    }
}