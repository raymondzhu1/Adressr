using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder) 
        {
            builder.HasKey(exam => exam.ExamID);

            builder.Property(exam => exam.Title).IsRequired().HasMaxLength(50);

            builder.HasOne(exam => exam.Creator).WithMany(user => user.CreatedExams).HasForeignKey(exam => exam.CreatorID).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(exam => exam.PrivateExamParticipants).WithMany(u => u.PrivateExams).UsingEntity(e => e.ToTable("PrivateExamParticipants"));
        }
    }
}
