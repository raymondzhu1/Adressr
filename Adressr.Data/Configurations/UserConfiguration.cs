using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserID);

            builder.Property(x => x.Username).IsRequired().HasMaxLength(30);

            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Password).IsRequired().HasColumnType("VARCHAR(255)");

            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
