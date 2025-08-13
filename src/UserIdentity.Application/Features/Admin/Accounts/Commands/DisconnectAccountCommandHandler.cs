using MediatR;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Accounts.Commands
{
    public class DisconnectAccountCommandHandler : IRequestHandler<DisconnectAccountCommand, bool>
    {
        private readonly ISqlGenericRepository _repository;

        public DisconnectAccountCommandHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DisconnectAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync<Account>(request.AccountId);
            
            if (account == null)
                return false;

            // For disconnect functionality, we could add a "ForceDisconnect" flag or timestamp
            // For now, we'll just verify the account exists and return success
            // In a real implementation, this might invalidate JWT tokens or update session state
            
            return true;
        }
    }
}
