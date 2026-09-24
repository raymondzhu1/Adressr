using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class OpeningConfiguration : IEntityTypeConfiguration<Opening>
    {
        public void Configure(EntityTypeBuilder<Opening> builder)
        {
            builder.HasKey(x => x.OpeningID);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Wage).HasPrecision(15, 2);

            builder.HasOne(x => x.Company).WithMany(x => x.Openings).HasForeignKey(x => x.CompanyID).IsRequired();
        }
    }
}
