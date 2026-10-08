using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.CompanyID);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);

            builder.Property(x => x.Description).IsRequired();

            builder.HasOne(x => x.Creator).WithMany(u => u.Companies).HasForeignKey(x => x.CreatorID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
