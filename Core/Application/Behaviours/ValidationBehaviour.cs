// <copyright file="ValidationBehaviour.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Behaviours
{
    using CleanArchCQRSMediatorAPI.Application.Exceptions;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using FluentValidation;
    using MediatR;

    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> validatators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validatators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (this.validatators.Count() > 0)
            {
                var ctx = new ValidationContext<TRequest>(request);
                var validationFailures = await Task.WhenAll(
                    this.validatators.Select(v => v.ValidateAsync(ctx)));
                var errors = validationFailures
                    .Where(validationResult => !validationResult.IsValid)
                    .SelectMany(validationResult => validationResult.Errors)
                    .Select(validationFailure => new ValidationError(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage))
                    .ToList();
                if (errors.Any())
                {
                    throw new Exceptions.ValidationException(errors);
                }
            }

            return await next();
        }
    }
}