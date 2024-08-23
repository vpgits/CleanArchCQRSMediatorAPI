// <copyright file="ResultT.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Shared
{
    public class Result<TValue> : Result
    {
        private readonly TValue? value;

        protected internal Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error) =>
            this.value = value;

        public TValue Value => this.IsSuccess
            ? this.value!
            : throw new InvalidOperationException("The value of the failure result cannot be accessed");
    }
}