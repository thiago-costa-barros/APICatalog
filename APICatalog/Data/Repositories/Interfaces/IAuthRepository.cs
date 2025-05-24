using APICatalog.Core.Entities.Models;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserToken?> InsertTokenRepository(int userId, TokenType type, string token, DateTime expirationDate);
        Task<IEnumerable<UserToken?>> GetTokensByUserIdRepository(int userId);
        Task<UserToken?> GetTokenRepository(string accessToken);
        Task<bool> RevokeLatestTokenByUserIdRepository(int userId, TokenType type);
        Task<bool> RevokeAllTokensByUserIdRepository(int userId);
    }
}
