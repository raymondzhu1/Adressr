using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class PlainTextEmployeeConfiguration : IEntityTypeConfiguration<PlainTextEmployee>
    {
        public void Configure(EntityTypeBuilder<PlainTextEmployee> builder)
        {
            builder.HasKey(plain => plain.PlainTextEmployeeID);

            builder.Property(plain => plain.FullName).IsRequired().HasMaxLength(50);

            builder.HasOne(plain => plain.Position).WithMany(p => p.PlainTexties).IsRequired();
        }
    }
}
