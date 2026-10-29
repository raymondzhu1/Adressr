using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class QuestionResponse : IQuestion
    {
        public int QuestionID { get; set; }
        public required string Description { get; set; }

        public AnswerType AnswerType { get; set; }

        public List<MultipleChoiceAnswerOptionResponse> MultipleChoiceAnswers { get; set; } = [];
    }
}
