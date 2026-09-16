using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Experience : IExperience
    {
        public required string CompanyName { get; set; }

        public required string RoleTitle { get; set; }

        public required string Description { get; set; }

        public bool Private { get; set; }

        public List<ISkill>? Skills { get; set; }

        public DateTime[] Tenure { get; set; } = new DateTime[2];

        public List<ITask>? PreviousTasks { get; set; }

        public ITask? CurrentTask { get; set; }
    }
}
