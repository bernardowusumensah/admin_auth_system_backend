using MediatR;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Subscriptions.Queries
{
    public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, IEnumerable<SubscriptionDto>>
    {
        private readonly ISqlGenericRepository _repository;

        public GetAllSubscriptionsQueryHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubscriptionDto>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _repository.GetAllAsync<Subscription>(
                include: q => q.Include(s => s.Account)
                                .ThenInclude(a => a.User)
            );

            return subscriptions.Select(s => new SubscriptionDto
            {
                Id = s.Id,
                SubscriptionType = s.SubscriptionType,
                Status = s.Status,
                Plan = s.Plan,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                AccountId = s.AccountId,
                AccountEmail = s.Account?.Email ?? "N/A",
                UserName = s.Account?.User != null 
                    ? $"{s.Account.User.FirstName} {s.Account.User.LastName}".Trim()
                    : "N/A"
            }).ToList();
        }
    }
}
