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
        }
    }
}
