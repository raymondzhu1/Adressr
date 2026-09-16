using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Opening : IOpening
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public List<ISkill>? Skills { get; set; }

        public int NumberOfOpenings { get; set; }

        public float Wage { get; set; }

        public required string WageType { get; set; }
    }
}
