using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class QuestionAskedEvent : EventBase
    {
        public Ulid QuestionId { get; }
        public string Title { get; }
        public string Content { get; }

        public QuestionAskedEvent(Ulid questionId, string title, string content)
        {
            QuestionId = questionId;
            Title = title;
            Content = content;
        }
    }
}
