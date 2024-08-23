// <copyright file="Member.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Domain.Entities
{
    using CleanArchCQRSMediatorAPI.Domain.Primitives;

    /// <summary>
    /// Types of members in the library.
    /// </summary>
    public enum MemberType
    {
        /// <summary>
        /// Regular library member with standard access.
        /// </summary>
        MEMBER,

        /// <summary>
        /// Library staff with special privileges.
        /// </summary>
        STAFF,
    }

    public class Member : BaseEntity
    {
        public Member(string username, MemberType memberType)
        {
            this.Username = username;
            this.MemberType = memberType;
        }

        public string Username { get; set; }

        public MemberType MemberType { get; set; }
    }
}