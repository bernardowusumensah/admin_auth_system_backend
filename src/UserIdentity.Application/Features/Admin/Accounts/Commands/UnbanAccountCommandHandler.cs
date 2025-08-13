using MediatR;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Accounts.Commands
{
    public class UnbanAccountCommandHandler : IRequestHandler<UnbanAccountCommand, bool>
    {
        private readonly ISqlGenericRepository _repository;

        public UnbanAccountCommandHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UnbanAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync<Account>(request.AccountId);
            
            if (account == null)
                return false;

            account.LockedOut = null;
            await _repository.UpdateAsync(account);
            
            return true;
        }
    }
}
