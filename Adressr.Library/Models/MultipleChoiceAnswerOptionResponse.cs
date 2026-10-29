using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class MultipleChoiceAnswerOptionResponse : IMultipleChoiceAnswerOption
    {
        public int MultipleChoiceAnswerOptionID { get; set; }
        public required string MultipleChoiceOptionText { get; set; }

        public required MultipleChoiceOption MultipleChoiceOption { get; set; }
    }
}
