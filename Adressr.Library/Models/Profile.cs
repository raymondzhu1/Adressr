using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class Profile : IProfile
    {
        public required string Name { get; set; }

        public required string ProfilePicLocation { get; set; }

        public required string Education { get; set; }

        public List<Experience>? Experience { get; set; }

        public List<Skill>? Skills { get; set; }

        public List<string>? Hobbies { get; set; }

        public List<string>? Interests { get; set; }
    }
}
