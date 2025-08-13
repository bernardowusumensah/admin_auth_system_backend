using MediatR;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Subscriptions.Commands
{
    public class CreateSubscriptionCommand : IRequest<SubscriptionDto>
    {
        public Guid AccountId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
