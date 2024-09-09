using Jgcarmona.Qna.Api.Common.Extensions;
using Jgcarmona.Qna.Application.Answers.Models;
using Jgcarmona.Qna.Application.Answers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUlid;

namespace Jgcarmona.Qna.Api.Controllers
{
    [Route("api/question/{questionId}/[controller]")]
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
            var profileId = User.GetProfileId();

            var command = new CreateAnswerCommand
            {
                QuestionId = questionId,
                Model = model,
                AuthorId = profileId
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create answer");

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{answerId}")]
        public async Task<IActionResult> Delete(string questionId, string answerId)
        {
            var command = new DeleteAnswerCommand
            {
                AnswerId = answerId
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Failed to delete answer.");
            }

            return Ok("Answer deleted successfully.");
        }

    }
}
