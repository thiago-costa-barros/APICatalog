using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;
using APICatalog.Core.Entities.Models;
using System.Runtime.CompilerServices;

namespace APICatalog.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetOrCreateUserSystemAsync(PublicEnum.UserType type, [CallerMemberName] string method = "");
    }
}
