using MediatR;
using UserIdentity.Application.DTOs.Admin;

namespace UserIdentity.Application.Features.Admin.Subscriptions.Queries
{
    public class GetAllSubscriptionsQuery : IRequest<IEnumerable<SubscriptionDto>>
    {
    }
}
