using Adressr.Data.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace Adressr.Data.Repository.Model
{
    public class UserSearchHit : ISearchHit
    {
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string ProfilePicLocation { get; set; }

        [SetsRequiredMembers]
        public UserSearchHit(string username, string name, string profilePicLocation)
        {
            Username = username;
            Name = name;
            ProfilePicLocation = profilePicLocation;
        }
    }
}
