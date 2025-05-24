using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;
using APICatalog.Data.Repositories.DAOs;
using APICatalog.Data.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace APICatalog.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO;
        public UserRepository(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public Task<User?> CreateUserRepository(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetOrCreateUserSystemRepository(PublicEnum.UserType type, [CallerMemberName] string method = "")
        {
            var userSystem = await _userDAO.GetOrCreateUserSystemAsync(type, method);

            return userSystem;
        }

        public Task<User?> GetUserByIdRepository(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailRepository(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByLoginRepository(string login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRepository(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUserRepository(int userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
