using MediatR;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Subscriptions.Commands
{
    public class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand, bool>
    {
        private readonly ISqlGenericRepository _repository;

        public CancelSubscriptionCommandHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _repository.GetByIdAsync<Subscription>(request.SubscriptionId);
            
            if (subscription == null)
                return false;

            subscription.Status = UserIdentity.Domain.Entities.SubscriptionStatus.Canceled;
            subscription.UpdatedOn = DateTime.UtcNow;

            await _repository.UpdateAsync(subscription);
            return true;
        }
    }
}
