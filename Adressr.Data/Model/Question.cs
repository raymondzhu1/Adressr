using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Question : IQuestion
    {
        public int QuestionID { get; set; }

        public required string Description { get; set; }
        public required AnswerType AnswerType { get; set; }

        public int ExamID { get; set; }
        public Exam Exam { get; set; } = null!;

        public ICollection<MultipleChoiceAnswerOption> AnswerOptions { get; set; } = new List<MultipleChoiceAnswerOption>();
    }
}
