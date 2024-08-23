// <copyright file="ReturnBookLibraryMemberCommandHandler.cs" company="vpgits">
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

    public class ReturnBookLibraryMemberCommandHandler : ICommandHandler<ReturnBookLibraryMemberCommand>
    {
        private readonly IGenericRepository<LibraryMember> libraryMemberRepository;
        private readonly IGenericRepository<Book> bookRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookLibraryMemberCommandHandler(IGenericRepository<LibraryMember> libraryMemberRepository, IGenericRepository<Book> bookRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.libraryMemberRepository = libraryMemberRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReturnBookLibraryMemberCommand command, CancellationToken cancellationToken)
        {
            var libraryMember = await this.libraryMemberRepository.GetByIdAsync(command.MemberId);
            if (libraryMember == null)
            {
                return Result.Failure(new Error("404", $"Library User with Guid {command.MemberId} not found"));
            }

            var book = await this.bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                return Result.Failure(new Error("404", $"Book with Guid {command.BookId} not found"));
            }

            if (!libraryMember.BorrowedBooks.Contains(book))
            {
                return Result.Failure(new Error("400", "The user has not borrowed this book"));
            }

            libraryMember.BorrowedBooks.Remove(book);
            book.BorrowedMember = null;
            book.BorrowedMemberId = null;
            book.IsAvailable = true;
            var task = this.unitOfWork.SaveChangesAsync(cancellationToken);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var libraryMemberDto = this.mapper.Map<LibraryMemberDto>(libraryMember);
                libraryMemberDto.CreatedAt = this.libraryMemberRepository.GetCreatedAtShadowProperty(libraryMember);
                libraryMemberDto.UpdatedAt = this.libraryMemberRepository.GetUpdatedAtShadowProperty(libraryMember);
                return Result.Success(libraryMemberDto);
            }

            return Result.Failure(new Error("500", "Unable to return book"));
        }
    }
}