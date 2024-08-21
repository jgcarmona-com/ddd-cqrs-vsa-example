using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Application.Features.Users;
using Jgcarmona.Qna.Application.Features.Users.Models;
using Jgcarmona.Qna.Application.Features.Users.Commands.RegisterUser;
using MediatR;
using Jgcarmona.Qna.Application.Features.Users.Queries;

namespace Jgcarmona.Qna.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {
            var command = new RegisterUserCommand { SignupModel = model };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create user");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var username = User?.Identity?.Name;
            if (username == null)
            {
                return BadRequest("User is not authenticated");
            }

            var query = new GetUserByUsernameQuery(username);
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
