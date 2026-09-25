using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data
{
    public class AdressrContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Opening> Openings { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Model.Task> Tasks { get; set; }

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
