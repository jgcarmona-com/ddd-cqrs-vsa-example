using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Application.Comments.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime PostedAt { get; set; }


        public static CommentModel FromEntity(Comment comment)
        {
            return new CommentModel
            {
                Id = comment.Id.ToString(),
                Content = comment.Content,
                AuthorId = comment.AuthorId.ToString(),
                PostedAt = comment.CreatedAt
            };
        }

        public static CommentModel FromCommentView(QuestionView.CommentView commentView)
        {
            return new CommentModel
            {
                Id = commentView.Id,
                Content = commentView.Content,
                AuthorId = commentView.AuthorId,
                AuthorName = commentView.AuthorName,
                PostedAt = commentView.PostedAt
            };
        }
    }
}
