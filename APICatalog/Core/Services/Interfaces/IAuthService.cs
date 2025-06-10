using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;
using APICatalog.Core.Entities.Models;

namespace APICatalog.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenReponseDTO> LoginService(AuthLoginRequestDTO loginRequestDTO);
        Task<UserToken> ValidateAccessTokenService(string token);
        Task<TokenReponseDTO> RefreshTokenService(AuthRefreshRequestDTO refreshTokenDTO);
        Task<TokenReponseDTO> GenerateAndSaveTokens(User user);
        Task<bool> LogoutService(int userId,string token);
        Task RevokeAllTokensByUserIdService(int userId);
    }
}
