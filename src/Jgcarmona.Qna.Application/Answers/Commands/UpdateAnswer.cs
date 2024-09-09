using Jgcarmona.Qna.Application.Answers.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Answers.Commands
{
    public class UpdateAnswerCommand : IRequest<AnswerModel>
    {
        public string AnswerId { get; set; }
        public string Content { get; set; }
    }

    public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, AnswerModel>
    {
        private readonly ICommandRepository<Answer> _answerRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UpdateAnswerCommandHandler> _logger;

        public UpdateAnswerCommandHandler(
            ICommandRepository<Answer> answerRepository,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor,
            ILogger<UpdateAnswerCommandHandler> logger)
        {
            _answerRepository = answerRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<AnswerModel> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetByIdAsync(Ulid.Parse(request.AnswerId));

            if (answer == null)
            {
                _logger.LogWarning($"Answer with ID {request.AnswerId} not found.");
                return null;
            }

            answer.Content = request.Content;
            await _answerRepository.UpdateAsync(answer);

            _logger.LogInformation($"Answer with ID {request.AnswerId} updated successfully.");

            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;
            var answerUpdatedEvent = new AnswerUpdatedEvent(answer)
            {
                CorrelationId = correlationId
            };

            await _eventDispatcher.DispatchAsync(answerUpdatedEvent);

            _logger.LogInformation($"AnswerUpdatedEvent dispatched for answer with ID {request.AnswerId}.");

            return AnswerModel.FromEntity(answer);
        }
    }
}
