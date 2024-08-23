// <copyright file="DeleteStaffMemberByIdCommandHandler.cs" company="vpgits">
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

    public class DeleteStaffMemberByIdCommandHandler : ICommandHandler<DeleteStaffMemberCommand>
    {
        private readonly IGenericRepository<StaffMember> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteStaffMemberByIdCommandHandler(IGenericRepository<StaffMember> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result> Handle(DeleteStaffMemberCommand command, CancellationToken cancellationToken)
        {
            var staffMember = await this.repository.GetByIdAsync(command.Id);
            if (staffMember == null)
            {
                return Result.Failure(new Error("403", $"Library Member not found for Guid {command.Id}"));
            }

            this.repository.Delete(staffMember);
            var task = this.unitOfWork.SaveChangesAsync(cancellationToken);
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
                return Result.Failure(new Error("500", $"Unable to delete Library Member with Guid {command.Id}"));
            }
        }
    }
}