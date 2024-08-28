namespace Jgcarmona.Qna.Application.Features.Questions.Models
{
    public class CreateQuestionModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
