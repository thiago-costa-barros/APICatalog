using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;
using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace APICatalog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }
        public async Task<User?> GetOrCreateUserSystemAsync(PublicEnum.UserType type, [CallerMemberName] string method = "")
        {
            var user = await _userRepository.GetOrCreateUserSystemRepository(type, method);
           
            return user;
        }

        public async Task<User?> RegisterUserService(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUserByLogin = string.IsNullOrEmpty(user.Login) ? await _userRepository.GetUserByLoginRepository(user.Login) : null;
            if (existingUserByLogin != null) throw new ArgumentException("User with this login already exists.");

            var existingUserByEmail = string.IsNullOrEmpty(user.Email) ? await _userRepository.GetUserByEmailRepository(user.Email) : null;
            if (existingUserByEmail != null) throw new ArgumentException("User with this email already exists.");

            var newUser = await _userRepository.CreateUserRepository(user);
            return newUser;
        }
    }
}
