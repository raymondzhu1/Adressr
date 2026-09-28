using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class ProfileHobbyConfiguration : IEntityTypeConfiguration<ProfileHobby>
    {
        public void Configure(EntityTypeBuilder<ProfileHobby> builder)
        {
            builder.HasKey(hobby => hobby.ProfileHobbyID);

            builder.Property(hobby => hobby.HobbyText).IsRequired().HasMaxLength(50);

            builder.HasOne(hobby => hobby.Profile).WithMany(p => p.Hobbies).HasForeignKey(hobby => hobby.ProfileID).IsRequired();
        }
    }
}
