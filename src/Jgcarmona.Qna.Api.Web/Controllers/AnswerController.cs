using Jgcarmona.Qna.Application.Features.Answers.Commands;
using Jgcarmona.Qna.Application.Features.Answers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jgcarmona.Qna.Api.Web.Controllers
{
    [Route("api/question/{id}/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string questionId, [FromBody] CreateAnswerModel model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var command = new CreateAnswerCommand
            {
                QuestionId = questionId,
                Model = model,
                AuthorId = userIdClaim
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create answer");

            return Ok(result);
        }
    }
}
