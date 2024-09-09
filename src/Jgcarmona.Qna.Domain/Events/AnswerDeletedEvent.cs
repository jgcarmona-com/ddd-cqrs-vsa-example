using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AnswerDeletedEvent : EventBase
    {
        public Answer Answer { get; }

        public AnswerDeletedEvent(Answer answer)
        {
            Answer = answer;
        }
    }
}
