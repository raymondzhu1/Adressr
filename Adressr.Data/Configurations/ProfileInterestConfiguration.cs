using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class ProfileInterestConfiguration : IEntityTypeConfiguration<ProfileInterest>
    {
        public void Configure(EntityTypeBuilder<ProfileInterest> builder) 
        {
            builder.HasKey(interest => interest.ProfileInterestID);

            builder.Property(interest => interest.InterestText).IsRequired().HasMaxLength(50);

            builder.HasOne(interest => interest.Profile).WithMany(p => p.Interests).HasForeignKey(interest => interest.ProfileID).IsRequired();
        }
    }
}
