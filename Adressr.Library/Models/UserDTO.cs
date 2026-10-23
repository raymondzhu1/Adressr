namespace Adressr.Library.Models
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public string? Email { get; set; }
    }
}
