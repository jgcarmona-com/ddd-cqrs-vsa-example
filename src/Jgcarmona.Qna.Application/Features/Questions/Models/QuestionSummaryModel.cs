using Jgcarmona.Qna.Application.Features.Comments.Models;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Questions.Models
{
    public class QuestionSummaryModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalVotes { get; set; }
        public List<CommentSummaryModel> Comments { get; set; } = new();

        public static QuestionSummaryModel FromEntity(Question question)
        {
            return new QuestionSummaryModel
            {
                Id = question.Id.ToString(),
                Title = question.Title,
                CreatedAt = question.CreatedAt,
                TotalVotes = question.Votes.Count,
                Comments = question.Comments.Select(CommentSummaryModel.FromEntity).ToList()
            };
        }
    }
}
