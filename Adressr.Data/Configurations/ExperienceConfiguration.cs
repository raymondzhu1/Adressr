using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(x => x.ExperienceID);

            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(30);

            builder.Property(x => x.RoleTitle).IsRequired().HasMaxLength(40);

            builder.Property(x => x.Description).IsRequired();

            builder.HasOne(x => x.Profile).WithMany(x => x.Experiences).HasForeignKey(x => x.ProfileID);
        }
    }
}
