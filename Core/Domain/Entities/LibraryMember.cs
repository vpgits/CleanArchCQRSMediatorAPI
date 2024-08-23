// <copyright file="LibraryMember.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Domain.Entities
{
    public class LibraryMember : Member
    {
        public LibraryMember(string username, MemberType memberType = MemberType.MEMBER)
            : base(username, memberType)
        {
            this.BorrowedBooks = new ();
        }

        public virtual List<Book> BorrowedBooks { get; set; }
    }
}