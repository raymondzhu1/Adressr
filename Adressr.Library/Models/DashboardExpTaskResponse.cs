namespace Adressr.Library.Models
{
    public class DashboardExpTaskResponse
    {
        public int ExperienceID { get; set; }
        public required string CompanyName { get; set; }
        public required string RoleTitle { get; set; }
        public int OpenTaskCount { get; set; }
        public List<DashboardTaskMiniResponse> Tasks { get; set; } = [];
    }
}
