// <copyright file="GetLibraryMemberByIdQuery.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Queries
{
    using CleanArchCQRSMediatorAPI.Application.Abstractions.Messaging;
    using CleanArchCQRSMediatorAPI.Application.Shared;

    public class GetLibraryMemberByIdQuery : IQuery<Result>
    {
        public GetLibraryMemberByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}