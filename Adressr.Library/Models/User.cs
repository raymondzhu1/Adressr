using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class User : IUser
    {
        public required string Username { get; set; }

        public required string Password { get; set; }

        public string? UserProfilePicLocation { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
