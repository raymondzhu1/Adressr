namespace Adressr.Data.Model
{
    public class PlainTextEmployee
    {
        public int PlainTextEmployeeID { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public DateTime HireDate { get; set; }

        public int CompanyID { get; set; } //used for unique composite key
        public Company Company { get; set; } = null!;

        public ICollection<Position> Positions { get; set; } = new List<Position>();
    }
}
