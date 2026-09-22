using Adressr.Library.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adressr.Data.Model
{
    public class Profile : IProfile
    {
        [Key]
        public int ProfileID { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }

        [MaxLength(300)]
        [Required]
        [Column(TypeName = "VARCHAR")]
        public required string ProfilePicLocation { get; set; }

        [MaxLength(70)]
        [Required]
        public required string Education { get; set; }

        [ForeignKey("TheUser")]
        public int UserID { get; set; }

        public User TheUser { get; set; } = null!;
    }
}
