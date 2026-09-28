namespace Adressr.Data.Model
{
    public class ProfileHobby
    {
        public int ProfileHobbyID { get; set; }

        public required string HobbyText { get; set; }

        public int ProfileID { get; set; }
        public Profile Profile { get; set; } = null!;
    }
}
