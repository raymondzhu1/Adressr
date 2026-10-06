using Adressr.Library.Interfaces;

namespace Adressr.Library.Models
{
    public class PreferenceDTO : IPreference
    {
        public bool DarkMode { get; set; }

        public bool ProfilePrivate { get; set; }
    }
}
