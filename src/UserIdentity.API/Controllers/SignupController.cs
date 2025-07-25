using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Application.DTOs;
using UserIdentity.Application.Features.Signup.Commands;

namespace UserIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SignupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<SignupResponse>> Signup([FromBody] SignupWithEmailAndPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
