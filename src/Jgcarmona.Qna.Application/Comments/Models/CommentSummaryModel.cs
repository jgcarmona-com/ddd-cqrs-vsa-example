using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Comments.Models
{
    public class CommentSummaryModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PostedAt { get; set; }
        public string TargetId { get; set; } // ID of the question or answer being commented on
        public string TargetType { get; set; } // "Question" or "Answer"

        public static CommentSummaryModel FromEntity(Comment comment)
        {

            string targetId;
            string targetType;
            string targetTitle;

            if (comment.QuestionId.HasValue)
            {
                targetId = comment.QuestionId.ToString();
                targetType = "Question";
                targetTitle = comment.Question.Title;
            }
            else if (comment.AnswerId.HasValue)
            {
                targetId = comment.AnswerId.ToString();
                targetType = "Answer";
                targetTitle = comment.Answer.Content.Length > 50 ? comment.Answer.Content.Substring(0, 50) + "..." : comment.Answer.Content;
            }
            else
            {
                targetId = string.Empty;
                targetType = "Unknown";
                targetTitle = string.Empty;
            }

            var model = new CommentSummaryModel
            {
                Id = comment.Id.ToString(),
                Content = comment.Content,
                Author = comment.Author.DisplayName,
                PostedAt = comment.CreatedAt,
                TargetId = targetId,
                TargetType = targetType
            };

            return model;
        }
    }
}
