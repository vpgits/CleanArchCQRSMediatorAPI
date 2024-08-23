// <copyright file="UpdateLibraryMemberCommandHandler.cs" company="vpgits">
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

    public class UpdateLibraryMemberCommandHandler : ICommandHandler<UpdateLibraryMemberCommand>
    {
        private readonly IGenericRepository<LibraryMember> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateLibraryMemberCommandHandler(IGenericRepository<LibraryMember> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateLibraryMemberCommand command, CancellationToken cancellationToken)
        {
            var libraryMember = await this.repository.GetByIdAsync(command.Id);
            if (libraryMember == null)
            {
                return Result.Failure(new Error("400", $"Unable to find the Library Member with Guid {command.Id}"));
            }

            libraryMember = this.mapper.Map<LibraryMember>(command);
            libraryMember = this.repository.Update(libraryMember);
            var task = this.unitOfWork.SaveChangesAsync();
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
                return Result.Failure(new Error("500", "Unable to update Library Member"));
            }
        }
    }
}