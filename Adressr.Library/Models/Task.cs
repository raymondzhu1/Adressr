using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Task : ITask
    {
        public required string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
