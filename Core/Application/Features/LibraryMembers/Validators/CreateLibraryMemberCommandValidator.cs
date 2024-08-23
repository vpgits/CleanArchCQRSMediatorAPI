// <copyright file="CreateLibraryMemberCommandValidator.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Validators
{
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using FluentValidation;

    internal class CreateLibraryMemberCommandValidator : AbstractValidator<CreateLibraryMemberCommand>
    {
        public CreateLibraryMemberCommandValidator()
        {
            this.RuleFor(m => m.Username).NotEmpty().NotNull().MaximumLength(256);
            this.RuleFor(m=>m.Password).NotEmpty().MaximumLength(256);
        }
    }
}