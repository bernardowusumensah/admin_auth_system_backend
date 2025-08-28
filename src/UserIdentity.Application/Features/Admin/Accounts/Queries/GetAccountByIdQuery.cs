using MediatR;
using UserIdentity.Application.DTOs.Admin;

namespace UserIdentity.Application.Features.Admin.Accounts.Queries
{
    public class GetAccountByIdQuery : IRequest<AccountDtoResponse?>
    {
        public Guid AccountId { get; set; }
    }
}
