using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class QuestionDeletedEvent : EventBase
    {
        public Question Question { get; }

        public QuestionDeletedEvent(Question question)
        {
            Question = question;
        }
    }
}
