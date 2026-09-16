using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class User : IUser
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string? UserProfilePicLocation { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
