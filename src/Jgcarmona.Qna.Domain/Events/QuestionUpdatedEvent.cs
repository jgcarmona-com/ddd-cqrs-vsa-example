using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class QuestionUpdatedEvent : EventBase
    {
        public Question Question { get; }

        public QuestionUpdatedEvent(Question question)
        {
            Question = question;
        }
    }
}
