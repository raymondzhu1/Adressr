using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class MultipleChoiceAnswerOption : IMultipleChoiceAnswerOption
    {
        public int MultipleChoiceAnswerOptionID { get; set; }

        public required string MultipleChoiceOptionText { get; set; }
        public required MultipleChoiceOption MultipleChoiceOption { get; set; }

        public int QuestionID { get; set; }
        public Question Question { get; set; } = null!;
    }
}
