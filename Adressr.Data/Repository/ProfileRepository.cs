using Adressr.Data.Repository.Interface;

namespace Adressr.Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AdressrContext _context;

        public ProfileRepository(AdressrContext context)
        {
            _context = context;
        }


    }
}
