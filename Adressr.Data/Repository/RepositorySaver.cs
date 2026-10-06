using Adressr.Data.Repository.Interface;

namespace Adressr.Data.Repository
{
    public class RepositorySaver : IRepositorySaver
    {
        private readonly AdressrContext _context;

        public RepositorySaver(AdressrContext context)
        {
            _context = context;
        }

        //Not my proudest work.
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
