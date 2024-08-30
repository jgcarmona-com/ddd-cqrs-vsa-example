using Jgcarmona.Qna.Api.Web.Extensions;
using Jgcarmona.Qna.Application.Features.Answers.Commands;
using Jgcarmona.Qna.Application.Features.Answers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jgcarmona.Qna.Api.Web.Controllers
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
    }
}
