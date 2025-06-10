using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;
using System.Runtime.CompilerServices;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdRepository(int userId);
        Task<User?> GetUserByLoginRepository(string login);
        Task<User?> GetUserByEmailRepository(string email);
        Task<User?> CreateUserRepository(User user);
        Task<User?> GetOrCreateUserSystemRepository(PublicEnum.UserType type, [CallerMemberName] string method = "");
        Task<User?> UpdateUserRepository(int userId, User user);
        Task<bool> RemoveUserRepository(int userId);
    }
}
