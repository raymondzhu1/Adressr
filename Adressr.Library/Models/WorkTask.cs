using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class WorkTask : IWorkTask
    {
        public required string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}
