using Jgcarmona.Qna.Application.Features.Comments.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Application.Features.Answers.Models
{

    public class AnswerModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Votes { get; set; }
        public bool IsAccepted { get; set; }
        public List<CommentModel> Comments { get; set; }


        public static AnswerModel FromEntity(Answer answer)
        {
            return new AnswerModel
            {
                Id = answer.Id.ToString(),
                Content = answer.Content,
                AuthorId = answer.AuthorId.ToString(),
                //Votes = answer.Votes.Count(),
                IsAccepted = answer.IsAccepted,
                Comments = answer.Comments.Select(CommentModel.FromEntity).ToList()
            };
        }

        public static AnswerModel FromAnswerView(QuestionView.AnswerView answerView)
        {
            return new AnswerModel
            {
                Id = answerView.EntityId,
                Content = answerView.Content,
                AuthorId = answerView.AuthorId,
                AuthorName = answerView.AuthorName,
                Votes = answerView.Votes,
                IsAccepted = answerView.IsAccepted,
                Comments = answerView.Comments.Select(CommentModel.FromCommentView).ToList()
            };
        }
    }

}
