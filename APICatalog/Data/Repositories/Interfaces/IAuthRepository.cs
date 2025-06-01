using APICatalog.Core.Common.Enum;
using APICatalog.Core.Entities.Models;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserToken?> InsertTokenRepository(int userId, TokenType type, string token, DateTime expirationDate);
        Task<IEnumerable<UserToken?>> GetTokensByUserIdRepository(int userId);
        Task<UserToken?> GetTokenRepository(string token, TokenType type);
        Task<bool> UpdateUserTokenRepository(int userId, string token, PublicEnum.TokenType type, PublicEnum.TokenStatus status);
        Task<bool> RevokeUserTokenRepository(int userId, string token, PublicEnum.TokenType type);
    }
}
