using Jgcarmona.Qna.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jgcarmona.Qna.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Authenticate([FromForm] string username, [FromForm] string password)
        {
            var command = new AuthenticateUserCommand(username, password);
            var tokenResponse = await _mediator.Send(command);

            if (tokenResponse == null)
                return Unauthorized(new { message = "Incorrect username or password" });

            return Ok(tokenResponse);
        }
    }
}
