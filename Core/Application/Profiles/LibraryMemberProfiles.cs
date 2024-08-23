// <copyright file="LibraryMemberProfiles.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Profiles
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.LibraryMembers.Commands;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    internal class LibraryMemberProfiles : Profile
    {
        public LibraryMemberProfiles()
        {
            this.CreateMap<CreateLibraryMemberCommand, LibraryMember>().ConstructUsing(src => new LibraryMember(src.Username ?? string.Empty, MemberType.MEMBER));
            this.CreateMap<LibraryMember, LibraryMemberDto>()
                .ForMember(dest => dest.BorrowedBookIds, opt => opt.MapFrom(src => src.BorrowedBooks.Select(book => book.Id).ToList()));
            this.CreateMap<LibraryMemberDto, LibraryMember>();
            this.CreateMap<UpdateLibraryMemberCommand, LibraryMember>();
        }
    }
}