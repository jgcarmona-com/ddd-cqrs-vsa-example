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
    public class DeleteAnswerCommand : IRequest<bool>
    {
        public string AnswerId { get; set; }
    }

    public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, bool>
    {
        private readonly ICommandRepository<Answer> _answerRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<DeleteAnswerCommandHandler> _logger;

        public DeleteAnswerCommandHandler(
            ICommandRepository<Answer> answerRepository,
            IEventDispatcher eventDispatcher,
            ILogger<DeleteAnswerCommandHandler> logger)
        {
            _answerRepository = answerRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetByIdAsync(Ulid.Parse(request.AnswerId));

            if (answer == null)
            {
                _logger.LogWarning($"Answer with ID {request.AnswerId} not found.");
                return false;
            }

            answer.IsDeleted = true;
            answer.UpdatedAt = DateTime.UtcNow;
            await _answerRepository.UpdateAsync(answer);

            _logger.LogInformation($"Answer with ID {request.AnswerId} deleted successfully.");
            await _eventDispatcher.DispatchAsync(new AnswerDeletedEvent(answer));

            return true;
        }
    }
}
