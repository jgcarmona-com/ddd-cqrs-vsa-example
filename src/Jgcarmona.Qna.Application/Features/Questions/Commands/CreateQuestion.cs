using Jgcarmona.Qna.Application.Features.Questions.Models;
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
    public class CreateQuestionCommand : IRequest<QuestionModel>
    {
        public CreateQuestionModel Model { get; set; } = null!;
        public Ulid AuthorId { get; set; }
    }

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionModel>
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

        public async Task<QuestionModel> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
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
            _logger.LogInformation($"Question with ID {newQuestion.Id} created.");
            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

            var questionCreatedEvent = new QuestionCreatedEvent(newQuestion, request.AuthorId.ToString())
            {
                CorrelationId = correlationId
            };
            await _eventDispatcher.DispatchAsync(questionCreatedEvent);
            _logger.LogInformation($"Question Created Event dispatched for question with ID {newQuestion.Id}.");
            return QuestionModel.FromEntity(newQuestion);
        }
    }
}
