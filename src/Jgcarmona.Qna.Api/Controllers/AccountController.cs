using Jgcarmona.Qna.Api.Common.Extensions;
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
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {
            var command = new SignupCommand { SignupModel = model };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create account");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var email = User?.Identity?.Name;
            var profileId = User.GetProfileId();
            if (email == null)
            {
                return BadRequest("Not authenticated!");
            }

            var query = new GetAccountByEmailQuery(email, profileId);
            var account = await _mediator.Send(query);

            if (account == null)
            {
                return NotFound("User not found");
            }

            return Ok(account);
        }
    }
}
