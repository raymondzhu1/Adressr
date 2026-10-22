using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class WorkersToPositionConfiguration : IEntityTypeConfiguration<WorkersToPosition>
    {
        public void Configure(EntityTypeBuilder<WorkersToPosition> builder)
        {
            builder.ToTable("WorkersToPosition");
            builder.HasKey(wp => new { wp.UserID, wp.PositionID });

            builder.Property(wp => wp.Wage).HasPrecision(15, 2);

            builder.HasOne(wp => wp.User).WithMany(user => user.Positions).HasForeignKey(wp => wp.UserID);
            builder.HasOne(wp => wp.Position).WithMany(position => position.Workers).HasForeignKey(wp => wp.PositionID);
        }
    }
}