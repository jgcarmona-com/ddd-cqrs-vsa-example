using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Jgcarmona.Qna.Application.Features.Questions.Commands.CreateQuestion;

namespace Jgcarmona.Qna.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionModel model)
        {
            var command = new CreateQuestionCommand { Model = model };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create question");

            return Ok(result);
        }
    }
}
