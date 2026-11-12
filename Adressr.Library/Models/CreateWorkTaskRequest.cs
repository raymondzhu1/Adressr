using System.ComponentModel.DataAnnotations;

namespace Adressr.Library.Models
{
    public class CreateWorkTaskRequest
    {
        [Required, MaxLength(1000)]
        public required string Description { get; set; }

        public bool IsDone { get; set; }

        public List<CreateWorkTaskRequest> SubTasks { get; set; } = [];

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(SubTasks.)
        //}
    }
}
