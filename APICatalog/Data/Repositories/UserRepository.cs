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

        public async Task<User?> CreateUserRepository(User user)
        {
            var createdUser = await _userDAO.CreateUserAsync(user);

            return createdUser;
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

        public async Task<User?> GetUserByEmailRepository(string email)
        {
            var user = await _userDAO.GetUserByEmailAsync(email);

            return user;
        }

        public async Task<User?> GetUserByLoginRepository(string login)
        {
            var user = await _userDAO.GetUserByLoginAsync(login);

            return user;
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
