namespace Adressr.Data.Model
{
    public class PlainTextEmployee
    {
        public int PlainTextEmployeeID { get; set; }

        public required string FullName { get; set; }

        public DateTime HireDate { get; set; }

        public int PositionID { get; set; }
        public Position Position { get; set; } = null!;
    }
}
