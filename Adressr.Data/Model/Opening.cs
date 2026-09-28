using Adressr.Library.Enums;
using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Opening : IOpening
    {
        public int OpeningID { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public decimal Wage { get; set; }

        public required WageType WageType { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; } = null!;

        public int PositionID { get; set; }

        public Position Position { get; set; } = null!;

        public ICollection<OpeningApplicants> OpeningApplicants { get; set; } = new List<OpeningApplicants>();

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
