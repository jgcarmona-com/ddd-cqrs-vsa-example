using Jgcarmona.Qna.Application.Features.Comments.Models;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Answers.Models
{
    public class AnswerSummaryModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public DateTime AnsweredAt { get; set; }
        public List<CommentSummaryModel> Comments { get; set; } = new();

        public static AnswerSummaryModel FromEntity(Answer answer)
        {
            return new AnswerSummaryModel
            {
                Id = answer.Id.ToString(),
                Content = answer.Content,
                Votes = answer.Votes.Count,
                AnsweredAt = answer.CreatedAt,
                Comments = answer.Comments.Select(CommentSummaryModel.FromEntity).ToList()
            };
        }
    }
}
