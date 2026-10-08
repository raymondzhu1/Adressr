using System.ComponentModel.DataAnnotations;

namespace Adressr.Library.Models
{
    public class CompanyRequest
    {
        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
