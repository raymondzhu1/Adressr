using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class ExamAttempt : IExamAttempt
    {
        public int ExamAttemptID { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public int? Score { get; set; }

        public int ExamID { get; set; }
        public Exam Exam { get; set; } = null!;

        public int UserID { get; set; }
        public User User { get; set; } = null!;

        public ICollection<SubmittedAnswer> SubmittedAnswers { get; set; } = new List<SubmittedAnswer>();
    }
}
