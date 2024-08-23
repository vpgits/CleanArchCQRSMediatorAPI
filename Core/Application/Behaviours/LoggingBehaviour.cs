// <copyright file="LoggingBehaviour.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Behaviours
{
    using CleanArchCQRSMediatorAPI.Application.Exceptions;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                this.logger.LogInformation(
                    "Starting Request {@RequestName},{@DateTimeUtc}",
                    typeof(TRequest).Name,
                    DateTime.UtcNow);
                var result = await next();
                if (result.IsFailure)
                {
                    var failure = result as Result;
                    this.logger.LogError(
                       "Request {@RequestName} failed: {@ErrorMessage},{@DateTimeUtc}",
                       typeof(TRequest).Name,
                       failure.Error,
                       DateTime.UtcNow);
                }

                this.logger.LogInformation(
                    "Completed Request {@RequestName},{@DateTimeUtc}",
                    typeof(TRequest).Name,
                    DateTime.UtcNow);

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(
                    ex,
                    "Exception happened during fulfilling {@RequestName}: {@ExceptionMessage}, {@DateTimeUtc}.\n{@ExceptionErrors},",
                    typeof(TRequest).Name,
                    ex.Message,
                    DateTime.UtcNow,
                    string.Join(',', (ex as ValidationException)?.Errors.Select(e => e.errorMessage) ?? Enumerable.Empty<string>()));
                throw;
            }
        }
    }
}