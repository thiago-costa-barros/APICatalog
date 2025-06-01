using APICatalog.Core.Common.Enum;
using APICatalog.Core.Entities.Models;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserToken?> InsertTokenRepository(int userId, TokenType type, string token, DateTime expirationDate, Guid identifier);
        Task<IEnumerable<UserToken?>> GetTokensByUserIdRepository(int userId);
        Task<UserToken?> GetTokenRepository(string token, TokenType type);
        Task<UserToken?> GetTokenByIdentifierRepository(Guid identifier, TokenType type);
        Task<bool> UpdateUserTokenRepository(int userId, Guid identifier, PublicEnum.TokenType type, PublicEnum.TokenStatus status);
        Task<bool> RevokeUserTokenRepository(int userId, Guid identifier, PublicEnum.TokenType type);
    }
}
