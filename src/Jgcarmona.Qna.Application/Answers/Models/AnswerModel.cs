using Jgcarmona.Qna.Application.Comments.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Application.Answers.Models
{
    public class AnswerModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Score { get; set; }
        public bool IsAccepted { get; set; }
        public List<CommentModel> Comments { get; set; }

        public static AnswerModel FromEntity(Answer answer)
        {
            var score = answer.Votes.Sum(v => v.IsUpvote ? 1 : -1);

            var model = new AnswerModel
            {
                Id = answer.Id.ToString(),
                Content = answer.Content,
                AuthorId = answer.AuthorId.ToString(),
                AuthorName = answer.Author.DisplayName, // Assuming you have access to Author's DisplayName
                Score = score,
                IsAccepted = answer.IsAccepted,
                Comments = answer.Comments.Select(CommentModel.FromEntity).ToList()
            };

            return model;
        }

        public static AnswerModel FromAnswerView(QuestionView.AnswerView answerView)
        {
            var model = new AnswerModel
            {
                Id = answerView.EntityId,
                Content = answerView.Content,
                AuthorId = answerView.AuthorId,
                AuthorName = answerView.AuthorName,
                Score = answerView.Votes, // Assuming Votes in the view represents the final score
                IsAccepted = answerView.IsAccepted,
                Comments = answerView.Comments.Select(CommentModel.FromCommentView).ToList()
            };

            return model;
        }
    }
}
