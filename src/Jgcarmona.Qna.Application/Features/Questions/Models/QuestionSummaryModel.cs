using Jgcarmona.Qna.Api.Models;
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
        public int AnswerCount { get; set; }
        public bool IsAnswered { get; set; }
        public List<Link> Links { get; set; } = new();

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

            model.Links.Add(new Link($"/api/questions/{model.Id}", "self", "GET"));
            model.Links.Add(new Link($"/api/questions/{model.Id}/answers", "answers", "GET"));

            return model;
        }
    }

}
