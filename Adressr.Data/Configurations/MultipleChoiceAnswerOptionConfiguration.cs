using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class MultipleChoiceAnswerOptionConfiguration : IEntityTypeConfiguration<MultipleChoiceAnswerOption>
    {
        public void Configure(EntityTypeBuilder<MultipleChoiceAnswerOption> builder)
        {
            builder.HasKey(MCAO => MCAO.MultipleChoiceAnswerOptionID);

            builder.Property(MCAO => MCAO.MultipleChoiceOptionText).IsRequired().HasMaxLength(250);

            builder.HasOne(MCAO => MCAO.Question).WithMany(q => q.AnswerOptions).HasForeignKey(MCAO => MCAO.QuestionID).IsRequired();

            builder.HasIndex(MCAO => new { MCAO.QuestionID, MCAO.MultipleChoiceOption }).IsUnique();
        }
    }
}
