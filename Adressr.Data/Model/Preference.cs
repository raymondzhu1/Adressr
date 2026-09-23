using Adressr.Library.Interfaces;

namespace Adressr.Data.Model
{
    public class Preference : IPreference
    {
        public bool DarkMode { get; set; }

        public bool ProfilePrivate { get; set; }

        public int UserID { get; set; }

        public User User { get; set; } = null!;
    }
}
