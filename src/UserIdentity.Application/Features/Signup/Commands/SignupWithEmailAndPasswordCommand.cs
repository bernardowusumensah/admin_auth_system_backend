using MediatR;
using System;
using UserIdentity.Application.DTOs;

namespace UserIdentity.Application.Features.Signup.Commands
{
    public record SignupWithEmailAndPasswordCommand(string Email, string Password, string FirstName, string LastName) : IRequest<SignupResponse>;

}




