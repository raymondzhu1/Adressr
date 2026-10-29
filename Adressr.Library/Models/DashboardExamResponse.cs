namespace Adressr.Library.Models
{
    public class DashboardExamResponse
    {
        public int ExamID { get; set; }
        public required string Title { get; set; }
        public int TimeLimitMinutes { get; set; }
    }
}
