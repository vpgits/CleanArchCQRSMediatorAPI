// <copyright file="UpdateBookCommandValidator.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Books.Validators
{
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Commands;
    using FluentValidation;

    internal class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            this.RuleFor(b => b.Author).NotEmpty().MaximumLength(256).WithMessage("Author Name must not exceed 256 character length");
            this.RuleFor(b => b.Title).NotEmpty().MaximumLength(256).WithMessage("Book Title must not exceed 256 character length");
            this.RuleFor(b => b.PublicationYear)
                .NotNull().WithMessage("Publication year must be provided.")
                .Must(value => value >= 1500 && value <= 2100)
                .WithMessage("Publication year must be between 1500 and 2100.");
            this.RuleFor(b => b.BookCategory).IsInEnum();
        }
    }
}