// <copyright file="UpdateStaffMemberCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Handlers
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    public class UpdateStaffMemberCommandHandler : ICommandHandler<UpdateStaffMemberCommand>
    {
        private readonly IGenericRepository<StaffMember> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateStaffMemberCommandHandler(IGenericRepository<StaffMember> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateStaffMemberCommand command, CancellationToken cancellationToken)
        {
            var staffMember = await this.repository.GetByIdAsync(command.Id);
            if (staffMember == null)
            {
                return Result.Failure(new Error("400", $"Unable to find the Library Member with Guid {command.Id}"));
            }

            staffMember = this.mapper.Map<StaffMember>(command);
            staffMember = this.repository.Update(staffMember);
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
                return Result.Failure(new Error("500", "Unable to update Library Member"));
            }
        }
    }
}