using MediatR;

namespace UserIdentity.Application.Features.Admin.Accounts.Commands
{
    public class DisconnectAccountCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
    }
}
