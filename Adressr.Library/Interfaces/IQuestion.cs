using Adressr.Library.Enums;

namespace Adressr.Library.Interfaces
{
    public interface IQuestion : IModel
    {
        //But why?

        public string Description { get; set; }

        public AnswerType AnswerType { get; set; }

        public string? OpenResponseAnswer { get; set; }

        public MultipleChoiceAnswers? MultipleChoiceAnswer { get; set; }
    }
}
