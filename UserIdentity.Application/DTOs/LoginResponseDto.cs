namespace UserIdentity.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public AccountDto Account { get; set; } = new AccountDto();
    }
}
