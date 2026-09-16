using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Company : ICompany
    {
        public required string Name { get; set; }

        public List<IPosition>? Positions { get; set; }
    }
}
