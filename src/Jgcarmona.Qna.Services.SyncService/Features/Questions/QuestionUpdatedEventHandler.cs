using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Services.Common;

namespace Jgcarmona.Qna.Services.SyncService.Features.Questions
{
    public class QuestionUpdatedEventHandler : IEventHandler<QuestionUpdatedEvent>
    {
        private readonly IQuestionViewRepository _questionViewRepository;
        private readonly IUserProfileViewRepository _userProfileViewRepository;
        private readonly ILogger<QuestionUpdatedEventHandler> _logger;

        public QuestionUpdatedEventHandler(
            IQuestionViewRepository questionViewRepository, 
            IUserProfileViewRepository userProfileViewRepository,
            ILogger<QuestionUpdatedEventHandler> logger)
        {
            _questionViewRepository = questionViewRepository;
            _userProfileViewRepository = userProfileViewRepository;
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

            var userProfileView = await _userProfileViewRepository.GetByIdAsync(domainEvent.Question.AuthorId);
            if (userProfileView != null)
            {
                var updatedUserProfileView = userProfileView.CreateNewVersion();

                var questionSummary = updatedUserProfileView.QuestionsAsked
                    .FirstOrDefault(q => q.Id == domainEvent.Question.Id.ToString());

                if (questionSummary != null)
                {
                    questionSummary.Title = domainEvent.Question.Title;
                    questionSummary.TotalVotes = newQuestionView.TotalVotes;
                }

                await _userProfileViewRepository.AddAsync(updatedUserProfileView);
                _logger.LogInformation($"UserProfile '{userProfileView.DisplayName}' updated with changes to question '{newQuestionView.Title}'.");
            }
            else
            {
                _logger.LogWarning($"User profile with ID {domainEvent.Question.AuthorId} not found in MongoDB.");
            }
        }
    }
}
