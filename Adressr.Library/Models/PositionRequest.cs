using Adressr.Library.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Adressr.Library.Models
{
    public class PositionRequest : IPosition
    {
        [Required]
        public required string Description { get; set; }

        [Required]
        [MaxLength(40)]
        public required string Title { get; set; }
    }
}
