using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adressr.Data.Configurations
{
    public class CorrectAnswerConfiguration : IEntityTypeConfiguration<CorrectAnswer>
    {
        public void Configure(EntityTypeBuilder<CorrectAnswer> builder) 
        {
            builder.HasKey(CA => CA.MultipleChoiceAnswerOptionID);

            builder.HasOne(CA => CA.MultipleChoiceAnswerOption).WithOne().HasForeignKey<CorrectAnswer>(CA => CA.MultipleChoiceAnswerOptionID);
        }
    }
}
