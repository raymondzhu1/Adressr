using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data
{
    public class AdressrContext: DbContext
    {
        //Stuff related to users
        public DbSet<User> Users { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        //Stuff related to companies
        public DbSet<Company> Companies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Opening> Openings { get; set; }

        //A bit of both of the above.
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }

        //Exam related DbSets
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<MultipleChoiceAnswerOption> MultipleChoiceAnswerOptions { get; set; }
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<SubmittedAnswer> SubmittedAnswers { get; set; }

        public AdressrContext(DbContextOptions<AdressrContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdressrContext).Assembly);
        }
    }
}
