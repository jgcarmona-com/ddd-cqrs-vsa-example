using Jgcarmona.Qna.Application.Features.Admin.Commands;
using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Application.Features.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUlid;

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
        public async Task<ActionResult<AccountModel>> EnableUser(Ulid userId)
        {
            var user = await _mediator.Send(new EnableUserCommand(userId));
            return Ok(user);
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(Ulid userId)
        {
            await _mediator.Send(new DeleteUserCommand(userId));
            return NoContent();
        }

        // Método para asignar roles
        [HttpPut("users/{userId}/assign-role")]
        public async Task<ActionResult<AccountModel>> AssignRole(Ulid userId, [FromBody] AssignRoleCommand command)
        {
            command.UserId = userId;
            var user = await _mediator.Send(command);
            return Ok(user);
        }
    }
}
