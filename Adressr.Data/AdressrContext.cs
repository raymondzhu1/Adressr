using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data
{
    public class AdressrContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public AdressrContext(DbContextOptions<AdressrContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.Preference).WithOne(x => x.TheUser).HasForeignKey<Preference>(x => x.UserID);
        }
    }
}
