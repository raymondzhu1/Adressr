using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Position : IPosition
    {
        public required string Description { get; set; }

        public List<ISkill>? Skills { get; set; }

        public int NumInCompany { get; set; }

        public int PotentialNumInCompany { get; set; }

        public List<IUser>? SiteAvailableEmployees { get; set; }

        public List<string>? PlainTextEmployees { get; set; }

        public required string Title { get; set; }
    }
}
