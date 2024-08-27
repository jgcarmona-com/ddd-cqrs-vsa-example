using Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;
using Jgcarmona.Qna.Domain.Views;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Questions.Queries
{
    public class GetQuestionByMonikerQuery : IRequest<QuestionView>
    {
        public string Moniker { get; set; }
    }

    public class GetQuestionByMonikerQueryHandler : IRequestHandler<GetQuestionByMonikerQuery, QuestionView>
    {
        private readonly IQuestionViewQueryRepository _questionRepository;
        private readonly ILogger<GetQuestionByMonikerQueryHandler> _logger;

        public GetQuestionByMonikerQueryHandler(IQuestionViewQueryRepository questionRepository, ILogger<GetQuestionByMonikerQueryHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<QuestionView> Handle(GetQuestionByMonikerQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByMonikerAsync(request.Moniker);
            if (question == null)
            {
                _logger.LogWarning($"Question with ID {request.Moniker} was not found.");
            }
            return question;
        }
    }
}
