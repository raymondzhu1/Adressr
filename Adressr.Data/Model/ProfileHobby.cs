namespace Adressr.Data.Model
{
    public class ProfileHobby
    {
        public int ProfileHobbyID { get; set; }

        public required string HobbyText { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int ProfileID { get; set; }
        public Profile Profile { get; set; } = null!;
    }
}
