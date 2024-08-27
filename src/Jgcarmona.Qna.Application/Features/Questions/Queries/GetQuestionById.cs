using Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;
using Jgcarmona.Qna.Domain.Views;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Questions.Queries
{
    public class GetQuestionByIdQuery : IRequest<QuestionView>
    {
        public Ulid QuestionId { get; set; }
    }

    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionView>
    {
        private readonly IQuestionViewQueryRepository _questionRepository;
        private readonly ILogger<GetQuestionByIdQueryHandler> _logger;

        public GetQuestionByIdQueryHandler(IQuestionViewQueryRepository questionRepository, ILogger<GetQuestionByIdQueryHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<QuestionView> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.QuestionId} was not found.");
            }
            return question;
        }
    }
}
