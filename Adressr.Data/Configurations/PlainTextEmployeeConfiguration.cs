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

            builder.Property(plain => plain.Email).IsRequired().HasMaxLength(150);

            builder.HasIndex(plain => new { plain.CompanyID, plain.Email }).IsUnique();

            builder.HasOne(plain => plain.Company).WithMany(company => company.PlainTexties).HasForeignKey(plain => plain.CompanyID).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(plain => plain.Positions).WithMany(p => p.PlainTexties).UsingEntity(join => join.ToTable("PlainTextEmployeesPositions"));
        }
    }
}
