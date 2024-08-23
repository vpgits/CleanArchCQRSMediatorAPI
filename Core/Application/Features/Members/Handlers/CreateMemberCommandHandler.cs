// <copyright file="CreateMemberCommandHandler.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.Members.Handlers
{
    using System.Diagnostics;
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Identity;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Persistence;
    using CleanArchCQRSMediatorAPI.Application.Features.Members.Commands;
    using CleanArchCQRSMediatorAPI.Application.Shared;
    using CleanArchCQRSMediatorAPI.Domain.Entities;
    using FluentValidation;

    public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand>
    {
        private readonly IGenericRepository<Member> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateMemberCommandHandler(IGenericRepository<Member> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
        {
            var member = this.mapper.Map<Member>(command);
            var savedMember = await this.repository.Add(member);
            var task = this.unitOfWork.SaveChangesAsync();
            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Result.Success(savedMember);
            }
            else
            {
                return Result.Failure(new Error("400", "Failed to add member"));
            }
        }
    }
}