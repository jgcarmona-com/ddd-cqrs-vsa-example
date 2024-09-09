using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AnswerUpdatedEvent : EventBase
    {
        public AnswerUpdatedEvent(Answer answer)
        {
            Answer = answer;
        }

        public Answer Answer { get; }
    }
}
