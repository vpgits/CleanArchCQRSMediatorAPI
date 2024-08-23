// <copyright file="ValidationException.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(IReadOnlyCollection<ValidationError> errors)
        {
            this.Errors = errors;
        }

        public IReadOnlyCollection<ValidationError> Errors { get; set; }
    }

    public record ValidationError(string propertyName, string errorMessage);
}