using MediatR;

namespace UserIdentity.Application.Features.Admin.Accounts.Commands
{
    public class BanAccountCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
    }
}
