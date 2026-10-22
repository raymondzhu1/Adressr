using Adressr.Library.Enums;

namespace Adressr.Data.Model
{
    public class WorkersToPosition
    {
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        public int PositionID { get; set; }
        public Position Position { get; set; } = null!;

        public DateTime HireDate { get; set; }
        public decimal? Wage { get; set; }
        public WageType? WageType { get; set; }
    }
}
