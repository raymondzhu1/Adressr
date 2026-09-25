using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class SubmittedAnswerConfiguration : IEntityTypeConfiguration<SubmittedAnswer>
    {
        public void Configure(EntityTypeBuilder<SubmittedAnswer> builder) 
        {
            builder.HasKey(SA => SA.SubmittedAnswerID);

            builder.HasOne(SA => SA.Question).WithMany().HasForeignKey(SA => SA.QuestionID).IsRequired();

            builder.HasOne(SA => SA.ExamAttempt).WithMany(attempt => attempt.SubmittedAnswers).HasForeignKey(SA => SA.ExamAttemptID).IsRequired();

            builder.HasMany(SA => SA.SelectedAnswers).WithMany().UsingEntity(x => x.ToTable("SubmittedAnswerSelectedAnswers"));
        }
    }
}
