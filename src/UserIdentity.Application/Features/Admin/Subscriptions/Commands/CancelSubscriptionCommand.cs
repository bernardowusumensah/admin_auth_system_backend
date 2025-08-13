using MediatR;

namespace UserIdentity.Application.Features.Admin.Subscriptions.Commands
{
    public class CancelSubscriptionCommand : IRequest<bool>
    {
        public Guid SubscriptionId { get; set; }
    }
}
