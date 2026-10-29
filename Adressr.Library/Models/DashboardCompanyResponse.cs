namespace Adressr.Library.Models
{
    public class DashboardCompanyResponse
    {
        public int CompanyID { get; set; }
        public required string Name { get; set; }
        public required string CompanyIconImageLocation { get; set; }
    }
}
