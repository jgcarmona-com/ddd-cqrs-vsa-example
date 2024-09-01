using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Answers.Models
{
    public class AnswerSummaryModel
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string ContentSnippet { get; set; }
        public int Votes { get; set; }
        public DateTime AnsweredAt { get; set; }

        public static AnswerSummaryModel FromEntity(Answer answer)
        {
            var model = new AnswerSummaryModel
            {
                Id = answer.Id.ToString(),
                QuestionId = answer.QuestionId.ToString(),
                QuestionTitle = answer.Question.Title,
                ContentSnippet = answer.Content.Length > 50 ? answer.Content.Substring(0, 50) + "..." : answer.Content,
                ////Votes = answer.Votes,  // TODO: Implement this
                AnsweredAt = answer.CreatedAt
            };


            return model;
        }
    }
}

