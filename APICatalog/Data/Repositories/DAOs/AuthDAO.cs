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
        public async Task<UserToken?> InsertTokenDAO(int userId, PublicEnum.TokenType type, string token, DateTime expirationDate)
        {
            var createToken = new UserToken
            {
                UserId = userId,
                JwtToken = token,
                ExpirationDate = expirationDate,
                Type = type,
                Status = PublicEnum.TokenStatus.Active
            };
            await _context.UserTokens.AddAsync(createToken);
            await _context.SaveChangesAsync();

            return createToken;
        }
    }
}
