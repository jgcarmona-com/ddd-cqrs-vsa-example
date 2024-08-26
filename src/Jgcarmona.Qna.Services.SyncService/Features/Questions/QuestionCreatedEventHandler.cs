using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Questions
{
    public class QuestionCreatedEventHandler : IEventHandler<QuestionCreatedEvent>
    {
        private readonly IQuestionRepository _questionViewRepository;
        private readonly ILogger<QuestionCreatedEventHandler> _logger;

        public QuestionCreatedEventHandler(
            IQuestionRepository questionViewRepository,
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
                Id = question.Id,
                Moniker = question.Moniker,
                Title = question.Title,
                Content = question.Content,
                TotalVotes = 0
            };

            await _questionViewRepository.AddAsync(questionView);

            _logger.LogInformation("Question '{Title}' added to MongoDB successfully.", questionView.Title);
        }
    }
}
