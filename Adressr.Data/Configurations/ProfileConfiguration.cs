using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.ProfileID);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.ProfilePicLocation).IsRequired().HasColumnType("VARCHAR(300)");

            builder.Property(x => x.Education).IsRequired().HasMaxLength(70);

            builder.HasOne(x => x.User).WithOne(x => x.Profile).HasForeignKey<Profile>(x => x.UserID).IsRequired();
        }
    }
}
