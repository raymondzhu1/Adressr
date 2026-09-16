using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Question : IQuestion
    {
        public required string Description { get; set; }

        public AnswerType AnswerType { get; set; }

        public string? OpenResponseAnswer { get; set; }

        public MultipleChoiceAnswers? MultipleChoiceAnswer { get; set; }
    }
}
