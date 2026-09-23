using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Company : ICompany
    {
        public int CompanyID { get; set; }

        public required string Name { get; set; }

        public ICollection<Position> Position { get; set; } = new List<Position>();
    }
}
