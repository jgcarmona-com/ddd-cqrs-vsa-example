using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;
using NUlid;

namespace Jgcarmona.Qna.Services.SyncService.Features.Answers
{
    public class AnswerCreatedEventHandler : IEventHandler<AnswerCreatedEvent>
    {
        private readonly IQuestionViewRepository _questionViewRepository;
        private readonly ILogger<AnswerCreatedEventHandler> _logger;

        public AnswerCreatedEventHandler(
            IQuestionViewRepository questionViewRepository,
            ILogger<AnswerCreatedEventHandler> logger)
        {
            _questionViewRepository = questionViewRepository;
            _logger = logger;
        }

        public async Task Handle(AnswerCreatedEvent domainEvent)
        {
            var newAnswer = domainEvent.Answer;
            var ExistingQuestionView = await _questionViewRepository.GetByIdAsync(newAnswer.QuestionId);
            if (ExistingQuestionView == null)
            {
                _logger.LogWarning($"Question View with ID {newAnswer.QuestionId} not found in MongoDB.");
                return;
            }

            var newQuestionView = ExistingQuestionView.CreateNewVersion();

            var answerView = new QuestionView.AnswerView
            {
                EntityId = newAnswer.Id.ToString(),
                Content = newAnswer.Content,
                AuthorId = newAnswer.AuthorId.ToString(),
                CreatedAt = newAnswer.CreatedAt,
            };

            newQuestionView.Answers.Add(answerView);
            newQuestionView.UpdatedAt = DateTime.UtcNow;

            await _questionViewRepository.AddAsync(newQuestionView);
            _logger.LogInformation($"Answer '{answerView.Content}' added to MongoDB successfully.");
        }
    }
}
