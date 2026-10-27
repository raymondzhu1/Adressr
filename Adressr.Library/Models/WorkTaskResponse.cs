using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class WorkTaskResponse : IWorkTask
    {
        public int WorkTaskID { get; set; }
        public required string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public List<WorkTaskResponse> SubTasks { get; set; } = [];
    }
}
