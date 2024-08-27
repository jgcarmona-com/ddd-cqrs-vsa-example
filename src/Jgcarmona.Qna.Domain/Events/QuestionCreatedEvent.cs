using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class QuestionCreatedEvent : EventBase
    {
        public Question Question { get; }
        public string AuthorId { get; }

        public QuestionCreatedEvent(Question question, string authorId)
        {
            Question = question;
            AuthorId = authorId;
        }
    }
}
