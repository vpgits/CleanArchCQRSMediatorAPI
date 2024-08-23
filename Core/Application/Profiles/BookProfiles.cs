// <copyright file="BookProfiles.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Application.Profiles
{
    using AutoMapper;
    using CleanArchCQRSMediatorAPI.Application.Dtos;
    using CleanArchCQRSMediatorAPI.Application.Features.Books.Commands;
    using CleanArchCQRSMediatorAPI.Domain.Entities;

    internal class BookProfiles : Profile
    {
        public BookProfiles()
        {
            this.CreateMap<Book, BookDto>();
            this.CreateMap<Book, Book>().ForMember(destination => destination.Id, opt => opt.Ignore());
            this.CreateMap<CreateBookCommand, Book>();
        }
    }
}