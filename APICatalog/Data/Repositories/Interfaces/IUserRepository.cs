using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common;
using System.Runtime.CompilerServices;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByLoginAsync(string login);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> CreateUserAsync(User user);
        Task<User?> GetOrCreateUserSystemAsync(PublicEnum.UserType type, [CallerMemberName] string method = "");
        Task<User?> UpdateUserAsync(int userId, User user);
        Task<bool> RemoveUserAsync(int userId);
    }
}
