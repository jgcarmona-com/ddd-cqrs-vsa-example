using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class QuestionCreatedEvent : EventBase
    {
        public Question Question { get; }

        public QuestionCreatedEvent(Question question)
        {
            Question = question;
        }
    }
}
