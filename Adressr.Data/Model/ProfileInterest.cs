namespace Adressr.Data.Model
{
    public class ProfileInterest
    {
        public int ProfileInterestID { get; set; }

        public required string InterestText { get; set; }

        public int ProfileID { get; set; }
        public Profile Profile { get; set; } = null!;
    }
}
