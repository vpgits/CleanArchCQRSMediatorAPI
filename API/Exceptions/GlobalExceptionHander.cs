// <copyright file="GlobalExceptionHander.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Exceptions
{
    using System.Net;
    using CleanArchCQRSMediatorAPI.Application.Exceptions;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class GlobalExceptionHander : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "An unhandled error occurred",
                Detail = exception.Message,
            };
            switch (exception)
            {
                case BadHttpRequestException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = exception.GetType().Name;
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Title = exception.GetType().Name;
                    problemDetails.Detail = unauthorizedAccessException.Message;
                    break;
                case TimeoutException timeoutException:
                    problemDetails.Status = (int)HttpStatusCode.RequestTimeout;
                    problemDetails.Title = exception.GetType().Name;
                    problemDetails.Detail = timeoutException.Message;
                    break;
                case InvalidOperationException invalidOperationException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = exception.GetType().Name;
                    problemDetails.Detail = invalidOperationException.Message;
                    break;
                case ValidationException validationException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = exception.GetType().Name;
                    problemDetails.Detail = validationException.Message + ' ' + string.Join(',', validationException.Errors);
                    break;
                default:
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    break;
            }

            httpContext.Response.StatusCode = (int)problemDetails.Status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}