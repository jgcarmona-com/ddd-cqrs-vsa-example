using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Questions.Models
{
    public class QuestionSummaryModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalVotes { get; set; }
        public int AnswerCount { get; set; }
        public bool IsAnswered { get; set; }

        public static QuestionSummaryModel FromEntity(Question entity)
        {
            var model = new QuestionSummaryModel
            {
                Id = entity.Id.ToString(),
                Title = entity.Title,
                CreatedAt = entity.CreatedAt,
                TotalVotes = entity.Votes.Count,
                AnswerCount = entity.Answers.Count,
                IsAnswered = entity.Answers.Any(a => a.IsAccepted)
            };

            return model;
        }
    }

}
