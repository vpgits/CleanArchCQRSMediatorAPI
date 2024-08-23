// <copyright file="DeleteLibraryMemberByIdCommandHandler.cs" company="vpgits">
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

    public class DeleteLibraryMemberByIdCommandHandler : ICommandHandler<DeleteLibraryMemberCommand>
    {
        private readonly IGenericRepository<LibraryMember> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteLibraryMemberByIdCommandHandler(IGenericRepository<LibraryMember> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result> Handle(DeleteLibraryMemberCommand command, CancellationToken cancellationToken)
        {
            var libraryMember = await this.repository.GetByIdAsync(command.Id);
            if (libraryMember == null)
            {
                return Result.Failure(new Error("403", $"Library Member not found for Guid {command.Id}"));
            }

            this.repository.Delete(libraryMember);
            var task = this.unitOfWork.SaveChangesAsync(cancellationToken);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var libraryMemberDto = this.mapper.Map<LibraryMemberDto>(libraryMember);
                libraryMemberDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(libraryMember);
                libraryMemberDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(libraryMember);
                return Result.Success(libraryMemberDto);
            }
            else
            {
                return Result.Failure(new Error("500", $"Unable to delete Library Member with Guid {command.Id}"));
            }
        }
    }
}