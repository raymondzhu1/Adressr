namespace Adressr.Data.Model
{
    public class CorrectAnswer
    {
        public int MultipleChoiceAnswerOptionID { get; set; }

        public MultipleChoiceAnswerOption MultipleChoiceAnswerOption { get; set; } = null!;
    }
}
