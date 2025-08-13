using MediatR;

namespace UserIdentity.Application.Features.Admin.Accounts.Commands
{
    public class UnbanAccountCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
    }
}
