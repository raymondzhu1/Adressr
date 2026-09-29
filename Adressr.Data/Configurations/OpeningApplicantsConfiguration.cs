using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class OpeningApplicantsConfiguration : IEntityTypeConfiguration<OpeningApplicants>
    {
        public void Configure(EntityTypeBuilder<OpeningApplicants> builder)
        {
            builder.HasKey(opapp => new { opapp.OpeningID, opapp.ApplicantID }); //Same Applicant won't be able to reapply for position if they do get rejected cuz of composite key. Might wanna add some kind of reapply date to this table. Or delete after some date and automove to another table.

            builder.HasOne(opapp => opapp.Opening).WithMany(opening => opening.OpeningApplicants).HasForeignKey(opapp => opapp.OpeningID);
            builder.HasOne(opapp => opapp.Applicant).WithMany(applicant => applicant.OpeningsApplied).HasForeignKey(opapp => opapp.ApplicantID);
        }
    }
}
