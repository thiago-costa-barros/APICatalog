using APICatalog.API.DTOs;
using APICatalog.Core.Common.Enum;
using APICatalog.Core.Entities.Models;
using APICatalog.Data.Repositories.DAOs;
using APICatalog.Data.Repositories.Interfaces;

namespace APICatalog.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDAO _authDAO;
        public AuthRepository(AuthDAO authDAO)
        {
            _authDAO = authDAO;
        }
        public async Task<UserToken?> GetTokenRepository(string token, PublicEnum.TokenType type)
        {
            var userToken = await _authDAO.GetTokenDAO(token, type);

            return userToken;
        }

        public async Task<IEnumerable<UserToken?>> GetTokensByUserIdRepository(int userId)
        {
            var tokens = await _authDAO.GetAllTokensByUserIdDAO(userId);

            return tokens;
        }

        public async Task<UserToken?> InsertTokenRepository(int userId, PublicEnum.TokenType type, string token, DateTime expirationDate)
        {
            var userToken = await _authDAO.InsertTokenDAO(userId, type, token, expirationDate);

            return userToken;
        }

        public async Task<bool> RevokeUserTokenRepository(int userId, string token, PublicEnum.TokenType type)
        {
            await _authDAO.RevokeUserTokenDAO(token, type);
            return true;

        }

        public async Task<bool> UpdateUserTokenRepository(int userId, string token, PublicEnum.TokenType type , PublicEnum.TokenStatus status)
        {
            await _authDAO.UpdateUserTokenDAO(token, type, status);
            return true;

        }
    }
}
