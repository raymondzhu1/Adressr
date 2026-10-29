using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class ExamResponse : IExam
    {
        public int ExamID { get; set; }
        public required string Title { get; set; }
        public required List<QuestionResponse> Questions { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public int? MaxRetakes { get; set; }

        public bool Private { get; set; }

        public List<UserDTO>? Participants { get; set; }

        public UserDTO Creator { get; set; } = null!;
    }
}
