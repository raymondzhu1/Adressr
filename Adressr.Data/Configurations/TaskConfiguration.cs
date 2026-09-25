using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Model.Task>
    {
        public void Configure(EntityTypeBuilder<Model.Task> builder)
        {
            builder.HasKey(t => t.TaskID);

            builder.Property(t => t.Description).IsRequired().HasMaxLength(1000);

            builder.HasOne(t => t.Experience).WithMany(e => e.Tasks).HasForeignKey(t => t.ExperienceID).IsRequired();
        }
    }
}
