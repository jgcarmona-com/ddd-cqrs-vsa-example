using Jgcarmona.Qna.Application.Questions.Models;
using Jgcarmona.Qna.Domain.Repositories.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Questions.Queries
{
    public class GetQuestionByMonikerQuery : IRequest<QuestionModel>
    {
        public string Moniker { get; set; }
    }

    public class GetQuestionByMonikerQueryHandler : IRequestHandler<GetQuestionByMonikerQuery, QuestionModel>
    {
        private readonly IQuestionViewQueryRepository _questionRepository;
        private readonly ILogger<GetQuestionByMonikerQueryHandler> _logger;

        public GetQuestionByMonikerQueryHandler(IQuestionViewQueryRepository questionRepository, ILogger<GetQuestionByMonikerQueryHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<QuestionModel> Handle(GetQuestionByMonikerQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByMonikerAsync(request.Moniker);
            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.Moniker} was not found.");
            }
            return QuestionModel.FromQuestionView(question);
        }
    }
}
