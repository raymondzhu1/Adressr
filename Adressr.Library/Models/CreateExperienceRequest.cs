using System.ComponentModel.DataAnnotations;

namespace Adressr.Library.Models
{
    public class CreateExperienceRequest : IValidatableObject
    {
        [Required, MaxLength(30)]
        public required string CompanyName { get; set; }

        [Required, MaxLength(40)]
        public required string RoleTitle { get; set; }

        [Required]
        public required string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SkillRequest> Skills { get; set; } = [];

        public List<CreateWorkTaskRequest> Tasks { get; set; } = [];


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int requestTaskSubmissionLimit = 50;
            int requestSubTaskSubmissionLimit = 20;

            if(EndDate is not null && EndDate < StartDate)
            {
                yield return new ValidationResult("The end date cannot occur before the start date.", [nameof(EndDate)]);
            }

            bool hasOpenTasks = Tasks.Any(task => (task.SubTasks.Count == 0 && !task.IsDone) || task.SubTasks.Any(subTask => !subTask.IsDone));
            if(EndDate is not null && hasOpenTasks)
            {
                yield return new ValidationResult("A completed job cannot have open tasks.", [nameof(Tasks)]);
            }

            if(Tasks.Count > requestTaskSubmissionLimit || Tasks.Any(task => task.SubTasks.Count > requestSubTaskSubmissionLimit))
            {
                yield return new ValidationResult("Too many tasks submitted in one request.", [nameof(Tasks)]);
            }
        }
    }
}
