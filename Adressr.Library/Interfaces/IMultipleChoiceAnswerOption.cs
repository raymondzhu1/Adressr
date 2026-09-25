using Adressr.Library.Enums;

namespace Adressr.Library.Interfaces
{
    public interface IMultipleChoiceAnswerOption : IModel
    {
        public string MultipleChoiceOptionText { get; set; }

        public MultipleChoiceOption MultipleChoiceOption { get; set; }
    }
}
