using Jgcarmona.Qna.Application.Features.Answers.Models;
using Jgcarmona.Qna.Application.Features.Comments.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Application.Features.Questions.Models
{
    public class QuestionModel
    {
        public string Id { get; set; }
        public string Moniker { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Score { get; set; }
        public DateTime LastActivityAt { get; set; }
        public List<string> Tags { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public List<CommentModel> Comments { get; set; }

        #region Mappings

        public static QuestionModel FromEntity(Question question)
        {
            var score = question.Votes.Sum(v => v.IsUpvote ? 1 : -1);

            var model = new QuestionModel
            {
                Id = question.Id.ToString(),
                Moniker = question.Moniker,
                Title = question.Title,
                Content = question.Content,
                AuthorId = question.AuthorId.ToString(),
                AuthorName = question.Author.DisplayName, // Assuming you have access to Author's DisplayName
                Tags = question.Tags,
                Score = score,
                Answers = question.Answers.Select(AnswerModel.FromEntity).ToList(),
                Comments = question.Comments.Select(CommentModel.FromEntity).ToList(),
            };

            return model;
        }

        public static QuestionModel FromQuestionView(QuestionView questionView)
        {
            var model = new QuestionModel
            {
                Id = questionView.EntityId,
                Moniker = questionView.Moniker,
                Title = questionView.Title,
                Content = questionView.Content,
                AuthorId = questionView.AuthorId,
                AuthorName = questionView.AuthorName,
                Score = questionView.TotalVotes, // Assuming TotalVotes in the view represents the final score
                LastActivityAt = questionView.LastActivityAt,
                Tags = questionView.Tags,
                Answers = questionView.Answers.Select(AnswerModel.FromAnswerView).ToList(),
                Comments = questionView.Comments.Select(CommentModel.FromCommentView).ToList()
            };

            return model;
        }

        #endregion
    }
}
