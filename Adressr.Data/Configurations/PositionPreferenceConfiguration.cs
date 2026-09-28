using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class PositionPreferenceConfiguration : IEntityTypeConfiguration<PositionPreference>
    {
        public void Configure(EntityTypeBuilder<PositionPreference> builder)
        {
            builder.HasKey(pospref => pospref.PositionID);

            builder.HasOne(pospref => pospref.Position).WithOne(pos => pos.PositionPreference).HasForeignKey<PositionPreference>(p => p.PositionID);
        }
    }
}
