// <copyright file="UpdateLibraryMemberCommandValidator.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Validators
{
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using FluentValidation;

    internal class UpdateLibraryMemberCommandValidator : AbstractValidator<UpdateLibraryMemberCommand>
    {
        public UpdateLibraryMemberCommandValidator()
        {
            this.RuleFor(m => m.Books).NotNull();
            this.RuleFor(m => m.Name).NotEmpty().NotNull().MaximumLength(256);
        }
    }
}