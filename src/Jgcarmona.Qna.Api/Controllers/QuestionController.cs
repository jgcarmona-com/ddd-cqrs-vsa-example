﻿using Jgcarmona.Qna.Api.Common.Extensions;
using Jgcarmona.Qna.Application.Questions.Commands;
using Jgcarmona.Qna.Application.Questions.Queries;
using Jgcarmona.Qna.Application.Questions.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUlid;

namespace Jgcarmona.Qna.Api.Controllers
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
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionModel model)
        {
            var profileId = User.GetProfileId();

            var command = new CreateQuestionCommand
            {
                Model = model,
                AuthorId = Ulid.Parse(profileId)
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to create question");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!Ulid.TryParse(id, out var questionId))
            {
                return BadRequest("Invalid Question ID format.");
            }

            var query = new GetQuestionByIdQuery { QuestionId = questionId };
            var question = await _mediator.Send(query);

            if (question == null)
            {
                return NotFound("Question not found.");
            }

            return Ok(question);
        }

        [Authorize]
        [HttpGet("by-moniker/{moniker}")]
        public async Task<IActionResult> GetByMoniker(string moniker)
        {
            var query = new GetQuestionByMonikerQuery { Moniker = moniker };
            var question = await _mediator.Send(query);

            if (question == null)
            {
                return NotFound("Question not found.");
            }

            return Ok(question);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateQuestionModel model)
        {
            var command = new UpdateQuestionCommand
            {
                QuestionId = Ulid.Parse(id),
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest("Failed to update question");

            return Ok("Question updated successfully");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Ulid.TryParse(id, out var questionId))
            {
                return BadRequest("Invalid Question ID format.");
            }

            var command = new DeleteQuestionCommand { QuestionId = questionId };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Failed to delete question.");
            }

            return Ok("Question deleted successfully.");
        }
    }
}