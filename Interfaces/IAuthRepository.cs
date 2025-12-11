
using WebApiFinal.Models;

namespace WebApiFinal.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario?> ValidateUserAsync(string email, string password);
        Task<Usuario> CreateUserAsync(string email, string password);
    }
}
