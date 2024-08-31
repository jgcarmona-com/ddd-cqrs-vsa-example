using Jgcarmona.Qna.Application.Features.Questions.Models;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;
using Jgcarmona.Qna.Domain.Services;

namespace Jgcarmona.Qna.Application.Features.Questions.Commands
{  

    public class UpdateQuestionCommand : IRequest<QuestionModel>
    {
        public Ulid QuestionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
    }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionModel>
    {
        private readonly ICommandRepository<Question> _questionRepository;

        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UpdateQuestionCommandHandler> _logger;

        public UpdateQuestionCommandHandler(
            ICommandRepository<Question> questionRepository, 
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor,            
            ILogger<UpdateQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<QuestionModel> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.QuestionId} not found.");
                return null;
            }

            question.Title = request.Title;
            question.Content = request.Content;
            question.Tags = request.Tags;

            await _questionRepository.UpdateAsync(question);

            _logger.LogInformation($"Question with ID {request.QuestionId} was updated successfully.");

            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;
            var questionUpdatedEvent = new QuestionUpdatedEvent(question)
            {
                CorrelationId = correlationId
            };

            await _eventDispatcher.DispatchAsync(questionUpdatedEvent);

            _logger.LogInformation($"QuestionUpdatedEvent dispatched for question with ID {request.QuestionId}.");
            return QuestionModel.FromEntity(question);
        }
    }
}
