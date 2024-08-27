using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class CreateQuestionCommand : IRequest<QuestionResponse>
    {
        public CreateQuestionModel Model { get; set; } = null!;
        public Ulid AuthorId { get; set; }
    }

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionResponse>
    {
        private readonly ICommandRepository<Question> _questionRepository;
        private readonly IMonikerService _monikerService;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CreateQuestionCommandHandler> _logger;

        public CreateQuestionCommandHandler(
            ICommandRepository<Question> questionRepository,
            IMonikerService monikerService,
            IEventDispatcher eventDispatcher,
            ILogger<CreateQuestionCommandHandler> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _questionRepository = questionRepository;
            _monikerService = monikerService;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<QuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var newQuestion = new Question
            {
                Title = model.Title,
                Content = model.Content,
                AuthorId = request.AuthorId
            };

            var questionMoniker = await _monikerService.GenerateMonikerAsync<Question>(newQuestion.Title);
            newQuestion.SetMoniker(questionMoniker);

            await _questionRepository.AddAsync(newQuestion);

            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

            var questionCreatedEvent = new QuestionCreatedEvent(newQuestion, request.AuthorId.ToString())
            {
                CorrelationId = correlationId
            };
            await _eventDispatcher.DispatchAsync(questionCreatedEvent);

            return new QuestionResponse
            {
                Id = newQuestion.Id,
                Moniker = newQuestion.Moniker,
                Title = newQuestion.Title,
                Content = newQuestion.Content
            };
        }
    }

    public class QuestionResponse
    {
        public Ulid Id { get; set; }
        public string Moniker { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
