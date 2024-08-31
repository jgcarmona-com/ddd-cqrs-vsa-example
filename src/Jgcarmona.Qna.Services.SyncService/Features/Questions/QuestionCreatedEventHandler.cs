using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Questions
{
    public class QuestionCreatedEventHandler : IEventHandler<QuestionCreatedEvent>
    {
        private readonly IQuestionViewRepository _questionViewRepository;
        private readonly ILogger<QuestionCreatedEventHandler> _logger;

        public QuestionCreatedEventHandler(
            IQuestionViewRepository questionViewRepository,
            ILogger<QuestionCreatedEventHandler> logger)
        {
            _questionViewRepository = questionViewRepository;
            _logger = logger;
        }

        public async Task Handle(QuestionCreatedEvent domainEvent)
        {
            var question = domainEvent.Question;

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
        }
    }
}
