namespace Adressr.Data.Model
{
    public class OpeningApplicants
    {
        public DateTime DateApplied { get; set; } = DateTime.UtcNow;

        public bool? Approved { get; set; }

        public int ApplicantID { get; set; }
        public User Applicant { get; set; } = null!;

        public int OpeningID { get; set; }
        public Opening Opening { get; set; } = null!;
    }
}
