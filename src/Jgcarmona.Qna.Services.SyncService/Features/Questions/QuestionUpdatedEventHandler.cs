using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Events;
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

            var newQuestionView = new QuestionView
            {
                EntityId = existingQuestionView.EntityId,
                Moniker = existingQuestionView.Moniker,
                Title = domainEvent.Question.Title,
                Content = domainEvent.Question.Content,
                Tags = domainEvent.Question.Tags,
                Answers = existingQuestionView.Answers,
                Comments = existingQuestionView.Comments,
                AuthorId = existingQuestionView.AuthorId,
                CreatedAt = existingQuestionView.CreatedAt,
                TotalVotes = existingQuestionView.TotalVotes,
                UpdatedAt = DateTime.UtcNow,
                Version = existingQuestionView.Version + 1 // Increase the version number
            };

            await _questionViewRepository.AddAsync(newQuestionView);

            _logger.LogInformation("Question '{Title}' updated in MongoDB successfully.", newQuestionView.Title);
        }
    }
}
