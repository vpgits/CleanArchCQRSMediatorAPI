// <copyright file="StaffMember.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Domain.Entities
{
    /// <summary>
    /// Types of staff members in the library.
    /// </summary>
    public enum StaffMemberType
    {
        /// <summary>
        /// Staff member who is a minor or junior position.
        /// </summary>
        MINOR,

        /// <summary>
        /// Staff member in a management or senior role.
        /// </summary>
        MANAGEMENT,
    }

    public class StaffMember : Member
    {
        public StaffMember(string username, MemberType memberType, StaffMemberType staffType)
            : base(username, memberType)
        {
            this.StaffType = staffType;
        }

        public StaffMember(string username, StaffMemberType staffMemberType = StaffMemberType.MINOR, MemberType memberType = MemberType.STAFF)
            : base(username, memberType)
        {
            this.StaffType = staffMemberType;
        }

        public StaffMemberType StaffType { get; set; }
    }
}