using UserIdentity.Application.DTOs;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        // In a real-world scenario, this would come from a database
        private readonly List<User> _users =
        [
            new() {
                Id =  Guid.NewGuid(),
                FirstName = "admin",
                LastName = "password", // In a real system, this would be hashed
                
            }
        ];

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException("Username and password are required.");
            }


            var user = _users.FirstOrDefault(u =>
                u.FirstName.Equals(request.Username, StringComparison.OrdinalIgnoreCase) &&
                u.LastName == request.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            
            var token = "dummy-jwt-token";

            return await Task.FromResult(new LoginResponseDto
            {
                Token = token,
                Account = new AccountDto
                {
                    Id = "1",
                    Username = user.FirstName,
                    Role = "Admin"
                }
            });
        }
    }
}
