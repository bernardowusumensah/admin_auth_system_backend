using System;
using System.Collections.Generic;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.DTOs.Admin
{
    public class AccountDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmation { get; set; }
        public List<RequiredActionDto>? RequiredActions { get; set; }
        public List<SubscriptionDto>? Subscriptions { get; set; }
        public string? UserId { get; set; }
        public DateTime? LockedOut { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class UserDto
    {
        public string? UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? DisplayName { get; set; }
        public Gender? Gender { get; set; }
        public string? Avatar { get; set; }
        public AddressDto? Address { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
    }

    public class AddressDto
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }

    public class RequiredActionDto
    {
        public string? AccountId { get; set; }
        public RequiredActionType? RequiredActionType { get; set; }
    }

    public class SubscriptionDto
    {
        public Guid Id { get; init; }
        public SubscriptionType SubscriptionType { get; init; }
        public SubscriptionStatus Status { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? AccountId { get; set; }
        public string? AccountEmail { get; set; }
        public string? UserName { get; set; }
    }
}
