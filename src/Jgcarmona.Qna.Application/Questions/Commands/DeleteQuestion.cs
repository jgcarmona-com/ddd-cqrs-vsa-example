using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Questions.Commands
{
    public class DeleteQuestionCommand : IRequest<bool>
    {
        public Ulid QuestionId { get; set; }
    }

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly ICommandRepository<Question> _questionRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DeleteQuestionCommandHandler> _logger;

        public DeleteQuestionCommandHandler(
            ICommandRepository<Question> questionRepository,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor,
            ILogger<DeleteQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.QuestionId} not found.");
                return false;
            }
            question.IsDeleted = true;
            question.UpdatedAt = DateTime.UtcNow;
            await _questionRepository.UpdateAsync(question);
            _logger.LogInformation($"Question with ID {request.QuestionId} was deleted successfully.");

            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;
            var questionDeletedEvent = new QuestionDeletedEvent(question)
            {
                CorrelationId = correlationId
            };

            await _eventDispatcher.DispatchAsync(questionDeletedEvent);

            _logger.LogInformation($"QuestionDeletedEvent dispatched for question with ID {request.QuestionId}.");

            return true;
        }
    }
}
