using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Task : ITask
    {
        public int TaskID { get; set; }

        public required string Description { get; set; }

        public bool IsDone { get; set; }

        public int ExperienceID { get; set; }

        public Experience Experience { get; set; } = null!;
    }
}
