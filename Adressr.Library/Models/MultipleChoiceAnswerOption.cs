using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class MultipleChoiceAnswerOption : IMultipleChoiceAnswerOption
    {
        public required string MultipleChoiceOptionText { get; set; }

        public required MultipleChoiceOption MultipleChoiceOption { get; set; }
    }
}
