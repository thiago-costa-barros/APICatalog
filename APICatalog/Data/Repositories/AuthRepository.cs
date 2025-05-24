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
        public Task<UserToken?> GetTokenRepository(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserToken?>> GetTokensByUserIdRepository(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserToken?> InsertTokenRepository(int userId, PublicEnum.TokenType type, string token, DateTime expirationDate)
        {
            var userToken = await _authDAO.InsertTokenDAO(userId, type, token, expirationDate);

            return userToken;
        }

        public Task<bool> RevokeAllTokensByUserIdRepository(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeLatestTokenByUserIdRepository(int userId, PublicEnum.TokenType type)
        {
            throw new NotImplementedException();
        }
    }
}
