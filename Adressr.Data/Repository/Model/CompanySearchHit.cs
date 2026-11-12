using Adressr.Data.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace Adressr.Data.Repository.Model
{
    public class CompanySearchHit : ISearchHit
    {
        public int CompanyID { get; set; }
        public required string Name { get; set; }
        public required string CompanyIconImageLocation { get; set; }

        [SetsRequiredMembers]
        public CompanySearchHit(int companyId, string name, string companyIconImageLocation)
        {
            CompanyID = companyId;
            Name = name;
            CompanyIconImageLocation = companyIconImageLocation;
        }
    }
}
