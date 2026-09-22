using Adressr.Library.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adressr.Data.Model
{
    public class Preference : IPreference
    {
        [Required]
        public bool DarkMode { get; set; }

        [Required]
        public bool ProfilePrivate { get; set; }

        [ForeignKey("TheUser")]
        [Required]
        public int UserID { get; set; }

        public User TheUser { get; set; } = null!;
    }
}
