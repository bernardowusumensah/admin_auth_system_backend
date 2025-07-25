using System;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Interfaces;

public interface IAuthenticationHelper
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string GenerateJwtToken(Account account);
}


