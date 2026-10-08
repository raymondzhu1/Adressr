using Adressr.Library.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Adressr.Library.Models
{
    public class ProfileRequest : IProfile
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(70)]
        public required string Education { get; set; }
    }
}
