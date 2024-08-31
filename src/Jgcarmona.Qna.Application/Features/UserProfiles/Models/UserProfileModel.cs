using Jgcarmona.Qna.Api.Models;
using Jgcarmona.Qna.Application.Features.Answers.Models;
using Jgcarmona.Qna.Application.Features.Comments.Models;
using Jgcarmona.Qna.Application.Features.Questions.Models;
using Jgcarmona.Qna.Application.Features.Votes.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Application.Features.UserProfiles.Models
{
    public class UserProfileModel
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public List<QuestionSummaryModel> QuestionsAsked { get; set; } = new();
        public List<AnswerSummaryModel> AnswersGiven { get; set; } = new();
        public List<VoteSummaryModel> Votes { get; set; } = new();
        public List<CommentSummaryModel> Comments { get; set; } = new();
        public List<Link> Links { get; set; } = new();

        public static UserProfileModel FromEntity(UserProfile userProfile)
        {
            return new UserProfileModel
            {
                Id = userProfile.Id.ToString(),
                AccountId = userProfile.AccountId.ToString(),
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                DisplayName = userProfile.DisplayName,
                DateOfBirth = userProfile.DateOfBirth,
                ProfilePictureUrl = userProfile.ProfilePictureUrl,
                PhoneNumber = userProfile.PhoneNumber,
                Gender = userProfile.Gender,
                QuestionsAsked = userProfile.Questions.Select(QuestionSummaryModel.FromEntity).ToList(),
                AnswersGiven = userProfile.Answers.Select(AnswerSummaryModel.FromEntity).ToList(),
                Comments = userProfile.Comments.Select(CommentSummaryModel.FromEntity).ToList(),
                Votes = userProfile.Votes.Select(VoteSummaryModel.FromEntity).ToList()
            };
        }

        public static UserProfileModel FromView(UserProfileView view)
        {
            var model = new UserProfileModel
            {
                Id = view.EntityId,
                AccountId = view.AccountId,
                FirstName = view.FirstName,
                LastName = view.LastName,
                DisplayName = view.DisplayName,
                DateOfBirth = view.DateOfBirth,
                ProfilePictureUrl = view.ProfilePictureUrl,
                PhoneNumber = view.PhoneNumber,
                Gender = view.Gender,
                QuestionsAsked = view.QuestionsAsked.Select(q => new QuestionSummaryModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    CreatedAt = q.CreatedAt,
                    TotalVotes = q.TotalVotes,
                    AnswerCount = q.AnswerCount,
                    IsAnswered = q.IsAnswered,
                    Links = new List<Link>
            {
                new Link($"/api/questions/{q.Id}", "self", "GET"),
                new Link($"/api/questions/{q.Id}/answers", "answers", "GET")
            }
                }).ToList(),
                AnswersGiven = view.AnswersGiven.Select(a => new AnswerSummaryModel
                {
                    Id = a.Id,
                    QuestionId = a.QuestionId,
                    QuestionTitle = a.QuestionTitle,
                    ContentSnippet = a.ContentSnippet,
                    Votes = a.Votes,
                    AnsweredAt = a.AnsweredAt,
                    Links = new List<Link>
            {
                new Link($"/api/questions/{a.QuestionId}", "question", "GET"),
                new Link($"/api/questions/{a.QuestionId}/answers/{a.Id}", "self", "GET")
            }
                }).ToList(),
                Comments = view.Comments.Select(c => new CommentSummaryModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    Author = c.Author,
                    PostedAt = c.PostedAt,
                    TargetId = c.TargetId,
                    TargetType = c.TargetType,
                    Links = new List<Link>
            {
                new Link(c.TargetType == "Question" ? $"/api/questions/{c.TargetId}" : $"/api/answers/{c.TargetId}", c.TargetType.ToLower(), "GET"),
                new Link($"/api/comments/{c.Id}", "self", "GET")
            }
                }).ToList(),
                Votes = view.Votes.Select(v => new VoteSummaryModel
                {
                    Id = v.Id,
                    TargetId = v.TargetId,
                    TargetType = v.TargetType,
                    TargetTitle = v.TargetTitle,
                    Value = v.Value,
                    Links = new List<Link>
            {
                new Link(v.TargetType == "Question" ? $"/api/questions/{v.TargetId}" : $"/api/answers/{v.TargetId}", v.TargetType.ToLower(), "GET")
            }
                }).ToList()
            };

            model.Links.Add(new Link($"/api/userprofiles/{model.Id}", "self", "GET"));

            return model;
        }
    }
}
