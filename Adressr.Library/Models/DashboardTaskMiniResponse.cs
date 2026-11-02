namespace Adressr.Library.Models
{
    public class DashboardTaskMiniResponse
    {
        public int WorkTaskID { get; set; }
        public required string Description { get; set; }
        public bool IsDone { get; set; }
        public List<DashboardSubTaskResponse> SubTasks { get; set; } = [];
    }
}
