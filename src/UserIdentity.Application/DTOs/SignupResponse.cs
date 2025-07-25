namespace UserIdentity.Application.DTOs
{
    public record SignupResponse
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public bool Success { get; init; }
        public string Message { get; init; }
    }
}