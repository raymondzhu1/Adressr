using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Exam : IExam
    {
        public required List<IQuestion> Questions { get; set; }

        public float TimeLength { get; set; }

        public bool Private { get; set; }

        public List<IUser>? Participants { get; set; }
    }
}
