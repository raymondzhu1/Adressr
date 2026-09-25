using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Exam : IExam
    {
        public int ExamID { get; set; }

        public required string Title { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public bool Private { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
