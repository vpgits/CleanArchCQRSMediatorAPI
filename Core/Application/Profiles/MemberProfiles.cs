// <copyright file="MemberProfiles.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Profiles
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.Members.Commands;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    internal class MemberProfiles : Profile
    {
        public MemberProfiles()
        {
            this.CreateMap<CreateMemberCommand, Member>().ConstructUsing(src => new Member(src.Name ?? string.Empty, src.MemberType));
            this.CreateMap<Member, MemberDto>();
            this.CreateMap<Member, Member>();
        }
    }
}