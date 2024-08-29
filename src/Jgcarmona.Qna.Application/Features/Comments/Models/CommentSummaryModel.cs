using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Comments.Models
{
    public class CommentSummaryModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PostedAt { get; set; }

        public static CommentSummaryModel FromEntity(Comment comment)
        {
            return new CommentSummaryModel
            {
                Id = comment.Id.ToString(),
                Content = comment.Content,
                Author = comment.Author.DisplayName,
                PostedAt = comment.CreatedAt
            };
        }
    }
}
