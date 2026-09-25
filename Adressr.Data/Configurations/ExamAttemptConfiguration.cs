using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class ExamAttemptConfiguration : IEntityTypeConfiguration<ExamAttempt>
    {
        public void Configure(EntityTypeBuilder<ExamAttempt> builder)
        {
            builder.HasKey(attempt => attempt.ExamAttemptID);

            builder.HasOne(attempt => attempt.Exam).WithMany().HasForeignKey(attempt => attempt.ExamID).IsRequired();

            builder.HasOne(attempt => attempt.User).WithMany(u => u.ExamAttempts).HasForeignKey(attempt => attempt.UserID).IsRequired();

            builder.ToTable(table => table.HasCheckConstraint("CK_ExamAttempt_Score_NonNegative", "[Score] IS NULL OR [Score] >= 0")); //Will be surprised if this check is actually ever needed. Sanity check.
        }
    }
}
