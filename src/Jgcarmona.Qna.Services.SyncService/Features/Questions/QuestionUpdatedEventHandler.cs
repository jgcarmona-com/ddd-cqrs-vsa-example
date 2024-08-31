using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Questions
{
    public class QuestionUpdatedEventHandler : IEventHandler<QuestionUpdatedEvent>
    {
        private readonly IQuestionViewRepository _questionViewRepository;
        private readonly ILogger<QuestionUpdatedEventHandler> _logger;

        public QuestionUpdatedEventHandler(IQuestionViewRepository questionViewRepository, ILogger<QuestionUpdatedEventHandler> logger)
        {
            _questionViewRepository = questionViewRepository;
            _logger = logger;
        }

        public async Task Handle(QuestionUpdatedEvent domainEvent)
        {
            var existingQuestionView = await _questionViewRepository.GetByIdAsync(domainEvent.Question.Id);

            if (existingQuestionView == null)
            {
                _logger.LogWarning($"Question with ID {domainEvent.Question.Id} not found in MongoDB.");
                return;
            }

            var newQuestionView = existingQuestionView.CreateNewVersion();
            newQuestionView.Title = domainEvent.Question.Title;
            newQuestionView.Content = domainEvent.Question.Content;
            newQuestionView.Tags = domainEvent.Question.Tags;
            newQuestionView.LastActivityAt = DateTime.UtcNow;

            await _questionViewRepository.AddAsync(newQuestionView);

            _logger.LogInformation("Question '{Title}' updated in MongoDB successfully.", newQuestionView.Title);
        }
    }
}
