using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class User : IUser
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public Preference? Preference { get; set; }
        public Profile? Profile { get; set; }
    }
}
