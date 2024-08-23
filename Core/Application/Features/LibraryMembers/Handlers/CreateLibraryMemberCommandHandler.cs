// <copyright file="CreateLibraryMemberCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class CreateLibraryMemberCommandHandler : ICommandHandler<CreateLibraryMemberCommand>
    {
        private readonly IGenericRepository<LibraryMember> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;

        public CreateLibraryMemberCommandHandler(IGenericRepository<LibraryMember> repository, IMapper mapper, IUnitOfWork unitOfWork, IUserService userService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }

        public async Task<Result> Handle(CreateLibraryMemberCommand command, CancellationToken cancellationToken)
        {
            var result = await this.userService.RegisterLibraryMember(command.Username!, command.Password!);
            if (result.IsFailure)
            {
                return result;
            }

            var libraryMember = await this.repository.Add(this.mapper.Map<LibraryMember>(command));
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
                return Result.Failure(new Error("500", "Unable to save a new Library User"));
            }
        }
    }
}