using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Profile : IProfile
    {
        public int ProfileID { get; set; }

        public required string Name { get; set; }

        public required string ProfilePicLocation { get; set; }

        public required string Education { get; set; }

        public int UserID { get; set; }

        public User User { get; set; } = null!;
    }
}
