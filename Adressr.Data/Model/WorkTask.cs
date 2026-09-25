using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class WorkTask : IWorkTask
    {
        public int WorkTaskID { get; set; }

        public required string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedDate { get; set; }

        public int ExperienceID { get; set; }

        public Experience Experience { get; set; } = null!;
    }
}
