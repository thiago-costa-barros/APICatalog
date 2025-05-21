using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common;
using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace APICatalog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> GetOrCreateUserSystemAsync(PublicEnum.UserType type, [CallerMemberName] string method = "")
        {
            var user = await _userRepository.GetOrCreateUserSystemAsync(type, method);

            return user;
        }
    }
}
