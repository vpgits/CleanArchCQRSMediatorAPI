// <copyright file="CreateMemberCommandValidator.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Members.Validators
{
    using CleanArchCQRSMediatorAPI.Application.Features.Members.Commands;
    using FluentValidation;

    internal class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            this.RuleFor(m => m.MemberType).IsInEnum().WithMessage("Invalid Member Type");
            this.RuleFor(m => m.Name).NotEmpty().MaximumLength(256);
        }
    }
}