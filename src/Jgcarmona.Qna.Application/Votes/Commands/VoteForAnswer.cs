using MediatR;
using NUlid;

namespace Jgcarmona.Qna.Application.Votes.Commands
{
    public class VoteForAnswerCommand : IRequest<bool>
    {
        public string AnswerId { get; set; }
        public string AuthorId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
