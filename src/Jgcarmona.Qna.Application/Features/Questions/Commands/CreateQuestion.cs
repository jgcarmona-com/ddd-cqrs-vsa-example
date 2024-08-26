using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Ulid UserId { get; set; }
    }

    public class CreateQuestionCommand : IRequest<QuestionResponse>
    {
        public CreateQuestionModel Model { get; set; } = null!;
    }

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionResponse>
    {
        private readonly ICommandRepository<Question> _questionRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<CreateQuestionCommandHandler> _logger;

        public CreateQuestionCommandHandler(
            ICommandRepository<Question> questionRepository,
            IEventDispatcher eventDispatcher,
            ILogger<CreateQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task<QuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var newQuestion = new Question
            {
                Title = model.Title,
                Content = model.Content,
                UserId = model.UserId
            };

            await _questionRepository.AddAsync(newQuestion);

            // Dispatch the event
            var questionCreatedEvent = new QuestionCreatedEvent(newQuestion);
            await _eventDispatcher.DispatchAsync(questionCreatedEvent);

            return new QuestionResponse
            {
                Id = newQuestion.Id,
                Title = newQuestion.Title,
                Content = newQuestion.Content
            };
        }
    }

    public class QuestionResponse
    {
        public Ulid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
