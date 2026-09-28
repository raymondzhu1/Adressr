namespace Adressr.Data.Model
{
    public class PositionPreference
    {
        public bool CanPostOpening { get; set; }

        public int PositionID { get; set; }
        public Position Position { get; set; } = null!;
    }
}
