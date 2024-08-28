using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AnswerCreatedEvent : EventBase
    {
        public string Id { get; }
        public string Content { get; }
        public string QuestionId { get; }
        public string AuthorId { get; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public AnswerCreatedEvent(string id, string content, string questionId, string authorId, DateTime createdAt)
        {
            Id = id;
            Content = content;
            QuestionId = questionId;
            AuthorId = authorId;
            CreatedAt = createdAt;
        }
    }
}
