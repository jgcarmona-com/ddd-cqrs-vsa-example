﻿using Jgcarmona.Qna.Api.Models;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Votes.Models
{
    public class VoteSummaryModel
    {
        public string Id { get; set; }
        public string TargetId { get; set; } // ID of the question or answer being voted on
        public string TargetType { get; set; } // "Question" or "Answer"
        public string TargetTitle { get; set; } // The title of the question or content snippet of the answer
        public int Value { get; set; } // +1 for upvote, -1 for downvote
        public List<Link> Links { get; set; } = new();

        public static VoteSummaryModel FromEntity(Vote vote)
        {
            string targetId;
            string targetType;
            string targetTitle;

            if (vote.QuestionId.HasValue)
            {
                targetId = vote.QuestionId.ToString();
                targetType = "Question";
                targetTitle = vote.Question.Title; // Assuming you have access to the question's title
            }
            else if (vote.AnswerId.HasValue)
            {
                targetId = vote.AnswerId.ToString();
                targetType = "Answer";
                targetTitle = vote.Answer.Content.Length > 50 ? vote.Answer.Content.Substring(0, 50) + "..." : vote.Answer.Content; // Assuming you have access to the answer's content
            }
            else
            {
                targetId = string.Empty;
                targetType = "Unknown";
                targetTitle = string.Empty;
            }

            var model = new VoteSummaryModel
            {
                Id = vote.Id.ToString(),
                TargetId = targetId,
                TargetType = targetType,
                TargetTitle = targetTitle,
                Value = vote.IsUpvote ? 1 : -1
            };

            model.Links.Add(new Link(targetType == "Question" ? $"/api/questions/{targetId}" : $"/api/answers/{targetId}", targetType.ToLower(), "GET"));

            return model;
        }
    }
}
