using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(RegisterUserRequest request);
        Task<bool> VerifyIdentityAsync(string SomeIdentifier, string InputtedPassword);
    }
}
