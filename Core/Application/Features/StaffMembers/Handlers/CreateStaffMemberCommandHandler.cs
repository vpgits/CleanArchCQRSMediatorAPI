// <copyright file="CreateStaffMemberCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class CreateStaffMemberCommandHandler : ICommandHandler<CreateStaffMemberCommand>
    {
        private readonly IGenericRepository<StaffMember> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;

        public CreateStaffMemberCommandHandler(IGenericRepository<StaffMember> repository, IMapper mapper, IUnitOfWork unitOfWork, IUserService userService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }

        public async Task<Result> Handle(CreateStaffMemberCommand command, CancellationToken cancellationToken)
        {
            var result = await this.userService.RegisterStaffMember(command.Username!, command.Password!, command.Role ?? 0);
            if (result.IsFailure)
            {
                return result;
            }

            var staffMember = await this.repository.Add(this.mapper.Map<StaffMember>(command));
            var task = this.unitOfWork.SaveChangesAsync();
            await task;
            if (task.IsCompletedSuccessfully)
            {
                var staffMemberDto = this.mapper.Map<StaffMemberDto>(staffMember);
                staffMemberDto.CreatedAt = this.repository.GetCreatedAtShadowProperty(staffMember);
                staffMemberDto.UpdatedAt = this.repository.GetUpdatedAtShadowProperty(staffMember);
                return Result.Success(staffMemberDto);
            }
            else
            {
                return Result.Failure(new Error("500", "Unable to save a new Staff User"));
            }
        }
    }
}