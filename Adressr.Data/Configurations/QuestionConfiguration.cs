using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.QuestionID);

            builder.Property(q => q.Description).IsRequired().HasMaxLength(1000);

            builder.HasOne(q => q.Exam).WithMany(e => e.Questions).HasForeignKey(q => q.ExamID).IsRequired();
        }
    }
}
