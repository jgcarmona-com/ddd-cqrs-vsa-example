using Jgcarmona.Qna.Application.Features.Accounts.Commands.SignUp;
using Jgcarmona.Qna.Application.Features.Accounts.Models;
using Jgcarmona.Qna.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jgcarmona.Qna.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {
            var command = new SignupCommand { SignupModel = model };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create account");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> Me()
        {
            var username = User?.Identity?.Name;
            if (username == null)
            {
                return BadRequest("Not authenticated!");
            }

            var query = new GetAccountByNameQuery(username);
            var account = await _mediator.Send(query);

            if (account == null)
            {
                return NotFound("User not found");
            }

            return Ok(account);
        }
    }
}
