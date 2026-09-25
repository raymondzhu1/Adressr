using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class ExamAttempt : IExamAttempt
    {
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? Score { get; set; }
    }
}
