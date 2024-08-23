// <copyright file="BorrowBookLibraryMemberCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class BurrowBookLibraryMemberCommandHandler : ICommandHandler<BorrowBookLibrayMemberCommand>
    {
        private readonly IGenericRepository<LibraryMember> libraryMemberRepository;
        private readonly IGenericRepository<Book> bookRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public BurrowBookLibraryMemberCommandHandler(IGenericRepository<LibraryMember> libraryMemberRepository, IGenericRepository<Book> bookRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.libraryMemberRepository = libraryMemberRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(BorrowBookLibrayMemberCommand command, CancellationToken cancellationToken)
        {
            var libraryMember = await this.libraryMemberRepository.GetByIdAsync(command.MemberId);
            if (libraryMember == null)
            {
                return Result.Failure(new Error("404", $"Library Member not found for Guid {command.MemberId}"));
            }

            var book = await this.bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                return Result.Failure(new Error("404", $"Book not found for Guid {command.BookId}"));
            }

            if (!book.IsAvailable)
            {
                return Result.Failure(new Error("400", $"Book with Guid {command.BookId} is currently not available"));
            }

            book.IsAvailable = false;
            if (libraryMember.BorrowedBooks.Contains(book))
            {
                return Result.Failure(new Error("400", $"Book has already been borrowed by yourself"));
            }

            libraryMember.BorrowedBooks.Add(book);
            book.BorrowedMember = libraryMember;
            book.BorrowedMemberId = libraryMember.Id;
            var task = this.unitOfWork.SaveChangesAsync(cancellationToken);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var libraryMemberDto = this.mapper.Map<LibraryMemberDto>(libraryMember);
                libraryMemberDto.CreatedAt = this.libraryMemberRepository.GetCreatedAtShadowProperty(libraryMember);
                libraryMemberDto.UpdatedAt = this.libraryMemberRepository.GetUpdatedAtShadowProperty(libraryMember);
                return Result.Success(libraryMemberDto);
            }

            return Result.Failure(new Error("500", "Unable to burrow book"));
        }
    }
}