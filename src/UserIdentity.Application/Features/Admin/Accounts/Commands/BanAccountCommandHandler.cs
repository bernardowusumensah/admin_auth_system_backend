using MediatR;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Accounts.Commands
{
    public class BanAccountCommandHandler : IRequestHandler<BanAccountCommand, bool>
    {
        private readonly ISqlGenericRepository _repository;

        public BanAccountCommandHandler(ISqlGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(BanAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync<Account>(request.AccountId);
            
            if (account == null)
                return false;

            account.LockedOut = DateTime.UtcNow;
            await _repository.UpdateAsync(account);
            
            return true;
        }
    }
}
