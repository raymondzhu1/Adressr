using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class OpeningApplicantsConfiguration : IEntityTypeConfiguration<OpeningApplicants>
    {
        public void Configure(EntityTypeBuilder<OpeningApplicants> builder)
        {
            builder.HasKey(opapp => new { opapp.OpeningID, opapp.ApplicantID });

            builder.HasOne(opapp => opapp.Applicant).WithMany(applicant => applicant.OpeningsApplied).HasForeignKey(opapp => opapp.ApplicantID);
            builder.HasOne(opapp => opapp.Opening).WithMany(opening => opening.OpeningApplicants).HasForeignKey(opapp => opapp.OpeningID);
        }
    }
}
