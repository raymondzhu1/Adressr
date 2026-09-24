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

        public bool Private { get; set; }

        public int ProfileID { get; set; }

        public Profile Profile { get; set; } = null!;
    }
}
