using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AnswerCreatedEvent : EventBase
    {
        public Answer Answer { get; }

        public AnswerCreatedEvent(Answer answer)
        {
            Answer = answer;
        }
    }
}
