// <copyright file="StaffMemberProfiles.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Profiles
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.StaffMembers.Commands;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    internal class StaffMemberProfiles : Profile
    {
        public StaffMemberProfiles()
        {
            this.CreateMap<CreateStaffMemberCommand, StaffMember>().ConstructUsing(src => new StaffMember(src.Username ?? string.Empty, MemberType.STAFF, src.Role ?? 0));
            this.CreateMap<StaffMember, StaffMemberDto>();
            this.CreateMap<StaffMemberDto, StaffMember>();
            this.CreateMap<UpdateStaffMemberCommand, StaffMember>();
        }
    }
}