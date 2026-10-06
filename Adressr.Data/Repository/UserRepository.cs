using Adressr.Data.Repository.Interface;
using Adressr.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AdressrContext _context;

        public UserRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return Task.CompletedTask;
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetByUsernameOrEmailAsync(string SomeKindOfID)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Username == SomeKindOfID || u.Email == SomeKindOfID);
        }
    }
}
