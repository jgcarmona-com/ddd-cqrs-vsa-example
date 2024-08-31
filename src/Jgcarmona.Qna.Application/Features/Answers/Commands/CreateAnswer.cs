using Jgcarmona.Qna.Application.Features.Answers.Models;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;
using Jgcarmona.Qna.Domain.Services;

namespace Jgcarmona.Qna.Application.Features.Answers.Commands
{
    public class CreateAnswerCommand : IRequest<AnswerModel>
    {
        public string QuestionId { get; set; }
        public CreateAnswerModel Model { get; set; } = null!;
        public string AuthorId { get; set; }
    }

    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerModel>
    {
        private readonly ICommandRepository<Question> _questionRepository;
        private readonly ICommandRepository<Answer> _answerRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CreateAnswerCommandHandler> _logger;

        public CreateAnswerCommandHandler(
            ICommandRepository<Question> questionRepository,
            ICommandRepository<Answer> answerRepository,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CreateAnswerCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AnswerModel> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(Ulid.Parse(request.QuestionId));
            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.QuestionId} not found.");
                throw new Exception("Question not found.");
            }

            var newAnswer = new Answer
            {
                Content = request.Model.Content,
                AuthorId = Ulid.Parse(request.AuthorId),
                QuestionId = question.Id
            };

            await _answerRepository.AddAsync(newAnswer);
            _logger.LogInformation($"Answer with ID {newAnswer.Id} created for question {request.QuestionId}.");
            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

            var answerCreatedEvent = new AnswerCreatedEvent(
                newAnswer.Id.ToString(),
                newAnswer.Content,
                newAnswer.QuestionId.ToString(),
                newAnswer.AuthorId.ToString(),
                newAnswer.CreatedAt)
            {
                CorrelationId = correlationId
            };
            await _eventDispatcher.DispatchAsync(answerCreatedEvent);
            _logger.LogInformation($"Answer Created Event dispatched for answer with ID {newAnswer.Id}.");

            return AnswerModel.FromEntity(newAnswer);
        }
    }
}
