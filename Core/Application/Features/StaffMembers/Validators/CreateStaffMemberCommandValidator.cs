// <copyright file="CreateStaffMemberCommandValidator.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Validators
{
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using FluentValidation;

    internal class CreateStaffMemberCommandValidator : AbstractValidator<CreateStaffMemberCommand>
    {
        public CreateStaffMemberCommandValidator()
        {
            this.RuleFor(m => m.Username).NotEmpty().NotNull().MaximumLength(256);
        }
    }
}