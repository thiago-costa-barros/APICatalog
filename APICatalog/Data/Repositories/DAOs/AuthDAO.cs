using APICatalog.APICataolog.Data.Context;
using Microsoft.EntityFrameworkCore;
using APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;

namespace APICatalog.Data.Repositories.DAOs
{
    public class AuthDAO
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthDAO(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<UserToken?> InsertTokenDAO(int userId, PublicEnum.TokenType type, string token, DateTime expirationDate, Guid identifier)
        {
            var createToken = new UserToken
            {
                UserId = userId,
                Identifier = identifier,
                JwtToken = token,
                ExpirationDate = expirationDate,
                Type = type,
                Status = PublicEnum.TokenStatus.Active
            };
            await _context.UserTokens.AddAsync(createToken);
            await _context.SaveChangesAsync();

            return createToken;
        }
        public async Task<UserToken?> GetTokenDAO(string token, PublicEnum.TokenType type)
        {
            var userToken = await _context.UserTokens
                .Where(x => 
                x.JwtToken == token 
                && x.Type == type 
                && x.Status == PublicEnum.TokenStatus.Active)
                .FirstOrDefaultAsync();

            return userToken;
        }
        public async Task<UserToken?> GetTokenByIdentifierDAO(Guid identifier, PublicEnum.TokenType type)
        {
            var userToken = await _context.UserTokens
                .Where(x =>
                x.Identifier == identifier
                && x.Type == type
                && x.Status == PublicEnum.TokenStatus.Active)
                .FirstOrDefaultAsync();

            return userToken;
        }

        public async Task<IEnumerable<UserToken>> GetAllTokensByUserIdDAO(int userId)
        {
            var tokens = await _context.UserTokens
                .Where(x =>
                x.UserId == userId
                && x.Status == PublicEnum.TokenStatus.Active
                && x.DeletionDate == null)
                .AsNoTracking()
                .ToListAsync();

            return tokens;
        }

        public async Task<UserToken?> UpdateUserTokenDAO(Guid identifier, PublicEnum.TokenType type, PublicEnum.TokenStatus status)
        {
            var userToken = await GetTokenByIdentifierDAO(identifier, type);
            if (userToken != null)
            {
                userToken.Status = status;
                _context.UserTokens.Update(userToken);
                await _context.SaveChangesAsync();
            }
            return userToken;
        }

        public async Task<UserToken?> RevokeUserTokenDAO(Guid identifier, PublicEnum.TokenType type)
        {
            var userToken = await GetTokenByIdentifierDAO(identifier, type);
            if (userToken != null)
            {
                userToken.Status = PublicEnum.TokenStatus.Revoked;
                userToken.DeletionDate = DateTime.UtcNow;
                _context.UserTokens.Update(userToken);
                await _context.SaveChangesAsync();
            }
            return userToken;
        }
    }
}
