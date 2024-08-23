// <copyright file="AuthorizationConstants.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.Identity.Constants
{
    public static class AuthorizationConstants
    {
        public static class AuthorizationPolicies
        {
            public const string ManagementStaffOnly = "management-staff-only";
            public const string MinorStaffOnly = "minor-staff-only";
            public const string LibraryMemberOnly = "library-member-only";
            public const string GetAllBooks = "get-all-books";
            public const string IsStaff = "is-staff";
        }

        public static class AuthorizationRoles
        {
            public const string ManagementStaff = "management-staff";
            public const string MinorStaff = "minor-staff";
            public const string LibraryMember = "library-member";
        }
    }
}