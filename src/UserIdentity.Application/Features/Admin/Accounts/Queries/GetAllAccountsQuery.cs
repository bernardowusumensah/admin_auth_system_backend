using MediatR;
using UserIdentity.Application.DTOs.Admin;

namespace UserIdentity.Application.Features.Admin.Accounts.Queries
{
    public class GetAllAccountsQuery : IRequest<AccountsResponse>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? Filter { get; set; }
    }
}
