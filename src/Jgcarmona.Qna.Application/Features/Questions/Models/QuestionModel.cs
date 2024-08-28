using Jgcarmona.Qna.Application.Features.Answers.Models;
using Jgcarmona.Qna.Application.Features.Comments.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int TotalVotes { get; set; }
        public DateTime LastActivityAt { get; set; }
        public List<string> Tags { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public List<CommentModel> Comments { get; set; }

        #region Mappings


        public static QuestionModel FromEntity(Question question)
        {
            return new QuestionModel
            {
                Id = question.Id.ToString(),
                Moniker = question.Moniker,
                Title = question.Title,
                Content = question.Content,
                AuthorId = question.AuthorId.ToString(),
                Tags = question.Tags,
                TotalVotes = question.Votes.Count(),
                Answers = question.Answers.Select(AnswerModel.FromEntity).ToList(),
                Comments = question.Comments.Select(CommentModel.FromEntity).ToList()

            };
        }

        public static QuestionModel FromQuestionView(QuestionView questionView)
        {
            return new QuestionModel
            {
                Id = questionView.EntityId,
                Moniker = questionView.Moniker,
                Title = questionView.Title,
                Content = questionView.Content,
                AuthorId = questionView.AuthorId,
                AuthorName = questionView.AuthorName,
                TotalVotes = questionView.TotalVotes,
                LastActivityAt = questionView.LastActivityAt,
                Tags = questionView.Tags,
                Answers = questionView.Answers.Select(AnswerModel.FromAnswerView).ToList(),
                Comments = questionView.Comments.Select(CommentModel.FromCommentView).ToList()
            };
        }

        #endregion
    }
}
