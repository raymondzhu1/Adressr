using System.ComponentModel.DataAnnotations;

namespace Adressr.Library.Models
{
    public class RegisterUserRequest
    {
        [MaxLength(30)]
        public required string Username { get; set; }
        [MaxLength(150)]
        public required string Email { get; set; }
        [MaxLength(255)]
        public required string Password { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(70)]
        public string? Education { get; set; }
    }
}
