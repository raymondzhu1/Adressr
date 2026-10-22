using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class PositionResponse
    {
        // REMINDER TO MYSELF: Need to add some kind of confirmation table to the database or something like alerts or notifications.
        // so that anyone getting invited to a position in a company gets to accept or deny the request.
        public int PositionID { get; set; }

        public string? Description { get; set; }

        public required string Title { get; set; }

        public string? Summary { get; set; }

        public int NumInCompany { get; set; } //Really only necessary if the two lists are null.

        public int NumberOfOpenings { get; set; }

        public List<UserDTO>? SiteAvailableEmployees { get; set; }

        public List<PlainTextEmployeeResponse>? PlainTextEmployees { get; set; }
    }
}