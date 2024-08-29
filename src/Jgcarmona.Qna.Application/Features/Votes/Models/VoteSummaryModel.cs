using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Votes.Models
{
    public class VoteSummaryModel
    {
        public string Id { get; set; }
        public string TargetId { get; set; } // ID of the question or answer being voted on
        public int Value { get; set; } // +1 for upvote, -1 for downvote
    
        public static VoteSummaryModel FromEntity(Vote vote)
        {
            return new VoteSummaryModel
            {
                Id = vote.Id.ToString(),
                TargetId = vote.QuestionId?.ToString() ?? vote.AnswerId?.ToString() ?? string.Empty,
                Value = vote.IsUpvote ? 1 : -1
            };
        }
    }
}
