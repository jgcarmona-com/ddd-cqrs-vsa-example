using Jgcarmona.Qna.Application.Features.Questions.Models;
using Jgcarmona.Qna.Domain.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Questions.Queries
{
    public class GetQuestionByIdQuery : IRequest<QuestionModel>
    {
        public Ulid QuestionId { get; set; }
    }

    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionModel>
    {
        private readonly IQuestionViewQueryRepository _questionRepository;
        private readonly ILogger<GetQuestionByIdQueryHandler> _logger;

        public GetQuestionByIdQueryHandler(IQuestionViewQueryRepository questionRepository, ILogger<GetQuestionByIdQueryHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<QuestionModel> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.QuestionId} was not found.");
            }
            return QuestionModel.FromQuestionView(question);
        }
    }
}
