using Adressr.Library.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adressr.Data.Model
{
    public class User : IUser
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Username { get; set; }
        [Required]
        [MaxLength(255)]
        [Column(TypeName = "VARCHAR")]
        public required string Password { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public required Preference Preference { get; set; }
    }
}
