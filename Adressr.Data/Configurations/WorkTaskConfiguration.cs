using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class WorkTaskConfiguration : IEntityTypeConfiguration<Model.WorkTask>
    {
        public void Configure(EntityTypeBuilder<Model.WorkTask> builder)
        {
            builder.HasKey(t => t.WorkTaskID);

            builder.Property(t => t.Description).IsRequired().HasMaxLength(1000);

            builder.HasOne(t => t.Experience).WithMany(e => e.Tasks).HasForeignKey(t => t.ExperienceID).IsRequired();

            builder.HasOne(t => t.ParentTask).WithMany(t => t.SubTasks).HasForeignKey(t => t.ParentTaskID).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint("CK_WorkTask_NotOwnParent", "[ParentTaskID] IS NULL OR [ParentTaskID] <> [WorkTaskID]"));
        }
    }
}
