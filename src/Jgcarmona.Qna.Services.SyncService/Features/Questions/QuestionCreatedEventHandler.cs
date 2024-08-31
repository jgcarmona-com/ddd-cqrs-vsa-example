using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Questions
{
    public class QuestionCreatedEventHandler : IEventHandler<QuestionCreatedEvent>
    {
        private readonly IQuestionViewRepository _questionViewRepository;
        private readonly IUserProfileViewRepository _userProfileViewRepository;
        private readonly ILogger<QuestionCreatedEventHandler> _logger;

        public QuestionCreatedEventHandler(
            IQuestionViewRepository questionViewRepository,
            IUserProfileViewRepository userProfileViewRepository,
            ILogger<QuestionCreatedEventHandler> logger)
        {
            _questionViewRepository = questionViewRepository;
            _userProfileViewRepository = userProfileViewRepository;
            _logger = logger;
        }

        public async Task Handle(QuestionCreatedEvent domainEvent)
        {
            var question = domainEvent.Question;


            var userProfileView = await _userProfileViewRepository.GetByIdAsync(question.AuthorId);
            if (userProfileView == null)
            {
                _logger.LogWarning($"User profile with ID {question.AuthorId} not found in MongoDB.");
                return;
            }

            var questionView = new QuestionView
            {
                EntityId = question.Id.ToString(),
                Moniker = question.Moniker,
                Title = question.Title,
                Content = question.Content,
                TotalVotes = 0,
                AuthorId = question.AuthorId.ToString(),
                CreatedAt = question.CreatedAt
            };

            await _questionViewRepository.AddAsync(questionView);
            _logger.LogInformation("Question '{Title}' added to MongoDB successfully.", questionView.Title);

            var newUserProfileView = userProfileView.CreateNewVersion();

            newUserProfileView.QuestionsAsked.Add(new UserProfileView.QuestionSummary
            {
                Id = question.Id.ToString(),
                Title = question.Title,
                CreatedAt = question.CreatedAt,
                TotalVotes = 0,
                AnswerCount = 0,
                IsAnswered = false
            });

            await _userProfileViewRepository.AddAsync(newUserProfileView);
            _logger.LogInformation("UserProfile '{DisplayName}' updated with new question '{Title}'.", newUserProfileView.DisplayName, questionView.Title);
        }
    }
}
