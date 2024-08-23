// <copyright file="Book.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Domain.Entities
{
    using System.Text.Json.Serialization;
    using CleanArchCQRSMediatorAPI.Domain.Primitives;

    /// <summary>
    /// Represents different categories of books.
    /// </summary>
    public enum BookCategory
    {
        /// <summary>
        /// Books that fall under the fiction genre.
        /// </summary>
        FICTION,

        /// <summary>
        /// Books that provide historical information or narrative.
        /// </summary>
        HISTORY,

        /// <summary>
        /// Books intended for children.
        /// </summary>
        CHILD,
    }

    public class Book : BaseEntity
    {
        public Book(string title, string author, int publicationYear, BookCategory bookCategory)
        {
            this.Title = title;
            this.Author = author;
            this.PublicationYear = publicationYear;
            this.BookCategory = bookCategory;
            this.IsAvailable = true;
            this.BorrowedMember = null;
            this.BorrowedMemberId = null;
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PublicationYear { get; set; }

        public BookCategory BookCategory { get; set; }

        public bool IsAvailable { get; set; }

        [JsonIgnore]
        public virtual LibraryMember? BorrowedMember { get; set; }

        public Guid? BorrowedMemberId { get; set; }
    }
}