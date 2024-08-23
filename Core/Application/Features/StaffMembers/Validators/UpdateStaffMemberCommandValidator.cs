// <copyright file="UpdateStaffMemberCommandValidator.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Validators
{
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using FluentValidation;

    internal class UpdateStaffMemberCommandValidator : AbstractValidator<UpdateStaffMemberCommand>
    {
        public UpdateStaffMemberCommandValidator()
        {
            this.RuleFor(m => m.Name).NotEmpty().NotNull().MaximumLength(256);
        }
    }
}