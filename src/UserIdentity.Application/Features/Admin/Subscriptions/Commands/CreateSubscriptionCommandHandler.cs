using MediatR;
using UserIdentity.Application.DTOs.Admin;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Subscriptions.Commands
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, SubscriptionDto>
    {
        private readonly ISqlGenericRepository _repository;

        public CreateSubscriptionCommandHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<SubscriptionDto> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = new Subscription
            {
                Id = Guid.NewGuid(),
                AccountId = request.AccountId,
                SubscriptionType = request.SubscriptionType,
                Plan = request.SubscriptionPlan,
                Status = UserIdentity.Domain.Entities.SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow,
                EndDate = request.SubscriptionPlan switch
                {
                    UserIdentity.Domain.Entities.SubscriptionPlan.Monthly => DateTime.UtcNow.AddMonths(1),
                    UserIdentity.Domain.Entities.SubscriptionPlan.Yearly => DateTime.UtcNow.AddYears(1),
                    UserIdentity.Domain.Entities.SubscriptionPlan.Lifetime => DateTime.MaxValue,
                    _ => DateTime.UtcNow.AddMonths(1)
                },
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _repository.InsertAsync(subscription);

            return new SubscriptionDto
            {
                Id = subscription.Id,
                SubscriptionType = subscription.SubscriptionType,
                Status = subscription.Status,
                Plan = subscription.Plan,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate
            };
        }
    }
}
