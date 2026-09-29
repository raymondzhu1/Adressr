using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Exam : IExam
    {
        public int ExamID { get; set; }

        public required string Title { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public int? MaxRetakes { get; set; } //nullable to allow infinite retakes when this is set to null. 0 = one single attempt allowed. Attempts allowed = MaxRetakes + 1.

        public bool Private { get; set; }

        public int CreatorID { get; set; }
        public User Creator { get; set; } = null!;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<User> PrivateExamParticipants = new List<User>();
    }
}
