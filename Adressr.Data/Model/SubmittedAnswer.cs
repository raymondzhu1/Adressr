namespace Adressr.Data.Model
{
    public class SubmittedAnswer
    {
        public int SubmittedAnswerID { get; set; }

        public string? OpenResponseAnswer { get; set; }

        public int QuestionID { get; set; }
        public Question Question { get; set; } = null!;

        public int ExamAttemptID { get; set; }
        public ExamAttempt ExamAttempt { get; set; } = null!;

        public ICollection<MultipleChoiceAnswerOption> SelectedAnswers { get; set; } = new List<MultipleChoiceAnswerOption>();
    }
}
