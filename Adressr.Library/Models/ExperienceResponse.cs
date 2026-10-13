using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class ExperienceResponse : IExperience
    {
        public int ExperienceID { get; set; }
        public required string CompanyName { get; set; }

        public required string RoleTitle { get; set; }

        public required string Description { get; set; }

        public bool IsExported { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SkillResponse>? Skills { get; set; }

        public List<WorkTaskResponse>? PreviousTasks { get; set; }

        public List<WorkTaskResponse>? CurrentTasks { get; set; }
    }
}
