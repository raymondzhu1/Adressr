using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data
{
    public class AdressrContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public AdressrContext(DbContextOptions<AdressrContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            //modelBuilder.Entity<User>().HasOne(x => x.Preference).WithOne(x => x.User).HasForeignKey<Preference>(x => x.UserID);
            //testing if GitHub is fixed again.
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdressrContext).Assembly);
        }
    }
}
