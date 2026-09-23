using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Position : IPosition
    {
        public int PositionID { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Summary { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; } = null!;
    }
}
