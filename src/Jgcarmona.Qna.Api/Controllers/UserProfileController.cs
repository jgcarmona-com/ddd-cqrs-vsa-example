using Jgcarmona.Qna.Api.Common.Extensions;
using Jgcarmona.Qna.Application.UserProfiles.Commands;
using Jgcarmona.Qna.Application.UserProfiles.Models;
using Jgcarmona.Qna.Application.UserProfiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUlid;

namespace Jgcarmona.Qna.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] string id, [FromBody] UpdateUserProfileModel model)
        {
            var command = new UpdateUserProfileCommand(Ulid.Parse(id), model);

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound("Profile not found");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] string id)
        {
            var query = new GetUserProfileByIdQuery(Ulid.Parse(id));
            var profile = await _mediator.Send(query);

            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            return Ok(profile);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var profileId = User.GetProfileId();
            var query = new GetUserProfileByIdQuery(Ulid.Parse(profileId));
            var profile = await _mediator.Send(query);

            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            return Ok(profile);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromRoute] string id, [FromBody] UpdateUserProfileModel model)
        {
            var profileId = User.GetProfileId();
            var command = new UpdateUserProfileCommand(Ulid.Parse(id), model);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Profile not found");
            }

            return Ok(result);
        }
    }
}
