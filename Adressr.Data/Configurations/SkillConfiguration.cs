using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.SkillID);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        }
    }
}
