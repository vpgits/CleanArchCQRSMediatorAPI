// <copyright file="Result.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Shared
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if ((isSuccess && error != Error.None) ||
                (!isSuccess && error == Error.None))
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !this.IsSuccess;

        public Error Error { get; }

        public static Result Success() => new (true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) => new (value, true, Error.None);

        public static Result Failure(Error error) => new (false, error);
    }
}