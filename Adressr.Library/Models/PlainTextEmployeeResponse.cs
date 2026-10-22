namespace Adressr.Library.Models
{
    public class PlainTextEmployeeResponse
    {
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public DateTime HireDate { get; set; }
    }
}
