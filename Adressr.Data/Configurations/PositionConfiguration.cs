using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.PositionID);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(40);

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Summary).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.Company).WithMany(x => x.Position).HasForeignKey(x => x.CompanyID).IsRequired();
        }
    }
}
