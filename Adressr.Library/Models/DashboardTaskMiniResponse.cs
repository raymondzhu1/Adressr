namespace Adressr.Library.Models
{
    public class DashboardTaskMiniResponse
    {
        public int WorkTaskID { get; set; }
        public required string Description { get; set; }
        public int SubTasksDone { get; set; }
        public int SubTasksTotal { get; set; }
    }
}
