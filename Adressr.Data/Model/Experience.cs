using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Experience : IExperience
    {
        public int ExperienceID { get; set; }

        public required string CompanyName { get; set; }

        public required string RoleTitle { get; set; }

        public required string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsExported { get; set; } //Flag for if this experience is exported from career manager to profile. Make false by default plz.

        public int ProfileID { get; set; }

        public Profile Profile { get; set; } = null!;

        public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();

        public ICollection<Skill> Skills { get; set; } = new List<Skill>(); 
    }
}
