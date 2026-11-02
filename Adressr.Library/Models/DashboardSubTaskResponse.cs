namespace Adressr.Library.Models
{
    public class DashboardSubTaskResponse
    {
        public int WorkTaskID { get; set; }
        public required string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
