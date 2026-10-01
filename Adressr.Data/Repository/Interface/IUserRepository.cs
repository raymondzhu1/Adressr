using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameOrEmailAsync(string SomeKindOfID);
    }
}
