using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Answers
{
    public class AnswerCreatedEventHandler : IEventHandler<AnswerCreatedEvent>
    {
        private readonly IQuestionViewRepository _questionViewRepository;
        private readonly IUserProfileViewRepository _userProfileViewRepository;
        private readonly ILogger<AnswerCreatedEventHandler> _logger;

        public AnswerCreatedEventHandler(
            IQuestionViewRepository questionViewRepository,
            IUserProfileViewRepository userProfileViewRepository,
            ILogger<AnswerCreatedEventHandler> logger)
        {
            _questionViewRepository = questionViewRepository;
            _userProfileViewRepository = userProfileViewRepository;
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
                QuestionId = newAnswer.QuestionId.ToString(),
                Content = newAnswer.Content,
                AuthorId = newAnswer.AuthorId.ToString(),
                CreatedAt = newAnswer.CreatedAt,
            };

            newQuestionView.Answers.Add(answerView);
            newQuestionView.UpdatedAt = DateTime.UtcNow;

            await _questionViewRepository.AddAsync(newQuestionView);
            _logger.LogInformation($"Answer '{answerView.Content}' added to MongoDB successfully.");

            var userProfileView = await _userProfileViewRepository.GetByIdAsync(newAnswer.AuthorId);
            if (userProfileView != null)
            {
                var updatedUserProfileView = userProfileView.CreateNewVersion();

                updatedUserProfileView.AnswersGiven.Add(new UserProfileView.AnswerSummary
                {
                    Id = newAnswer.Id.ToString(),
                    QuestionId = newAnswer.QuestionId.ToString(),
                    QuestionTitle = newQuestionView.Title,
                    AnsweredAt = newAnswer.CreatedAt,
                    Votes = 0,
                });

                await _userProfileViewRepository.AddAsync(updatedUserProfileView);
                _logger.LogInformation($"UserProfile '{userProfileView.DisplayName}' updated with new answer.");
            }
            else
            {
                _logger.LogWarning($"User profile with ID {newAnswer.AuthorId} not found in MongoDB.");
            }
        }
    }
}
