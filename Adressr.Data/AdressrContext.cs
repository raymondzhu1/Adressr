using Microsoft.EntityFrameworkCore;

namespace Adressr.Data
{
    public class AdressrContext: DbContext
    {
        public AdressrContext(DbContextOptions<AdressrContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
