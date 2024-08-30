using Jgcarmona.Qna.Application.Features.Admin.Commands;
using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Application.Features.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jgcarmona.Qna.Api.Web.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<AccountModel>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpPut("users/{userId}/enable")]
        public async Task<ActionResult<AccountModel>> EnableUser(string userId)
        {
            var user = await _mediator.Send(new EnableAccountCommand(userId));
            return Ok(user);
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _mediator.Send(new DeleteUserCommand(userId));
            return NoContent();
        }

        [HttpPut("users/{userId}/assign-role/{role}")]
        public async Task<ActionResult<AccountModel>> AssignRole(string userId, string role)
        {
            var user = await _mediator.Send(new AssignRoleCommand(userId, role));
            return Ok(user);
        }
    }
}
