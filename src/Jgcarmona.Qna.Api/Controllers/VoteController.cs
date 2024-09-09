using Jgcarmona.Qna.Api.Common.Extensions;
using Jgcarmona.Qna.Application.Votes.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jgcarmona.Qna.Api.Controllers
{
    [Route("api/question/{questionId}")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("vote")]
        public async Task<IActionResult> VoteForQuestion(string questionId, [FromBody] VoteModel model)
        {  
            var profileId = User.GetProfileId();

            var command = new VoteForQuestionCommand
            {
                QuestionId = questionId,
                AuthorId = profileId,
                IsUpvote = model.IsUpvote
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Failed to vote for the question.");
            }

            return Ok("Vote registered for the question.");
        }

        [Authorize]
        [HttpPost("answer/{answerId}/vote")]
        public async Task<IActionResult> VoteForAnswer(string questionId, string answerId, [FromBody] VoteModel model)
        {
            var profileId = User.GetProfileId();

            var command = new VoteForAnswerCommand
            {              
                AnswerId = answerId,
                AuthorId = profileId,
                IsUpvote = model.IsUpvote
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Failed to vote for the answer.");
            }

            return Ok("Vote registered for the answer.");
        }
    }
}
