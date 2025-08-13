using System;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.DTOs.Requests
{
    public class CreateSubscriptionRequest
    {
        public Guid AccountId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }

    public class CancelSubscriptionRequest
    {
        public Guid SubscriptionId { get; set; }
    }

    public class GetAccountsRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? Filter { get; set; }
    }
}
