using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common;
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

        public Task<User?> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetOrCreateUserSystemAsync(PublicEnum.UserType type, [CallerMemberName] string method = "")
        {
            var userSystem = await _userDAO.GetOrCreateUserSystemAsync(type, method);

            return userSystem;
        }

        public Task<User?> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByLoginAsync(string login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUserAsync(int userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
