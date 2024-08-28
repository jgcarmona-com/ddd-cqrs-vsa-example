namespace Jgcarmona.Qna.Application.Features.Questions.Models
{
    public class UpdateQuestionModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
    }
}
