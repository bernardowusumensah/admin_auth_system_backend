using System;
using UserIdentity.Application.DTOs;

namespace UserIdentity.Application.Interfaces;

public interface IAuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
