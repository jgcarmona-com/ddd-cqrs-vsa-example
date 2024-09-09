using MediatR;

namespace Jgcarmona.Qna.Application.Votes.Commands
{
    public class VoteForQuestionCommand : IRequest<bool>
    {
        public string QuestionId { get; set; }
        public string AuthorId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
