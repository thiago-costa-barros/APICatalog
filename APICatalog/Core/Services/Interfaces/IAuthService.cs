using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Enum;

namespace APICatalog.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenReponseDTO> LoginService(AuthLoginRequestDTO loginRequestDTO);
        Task<TokenValidationResultDTO> ValidateTokenService(string token);
        Task<TokenReponseDTO> RefreshTokenService(AuthRefreshRequestDTO refreshTokenDTO);
        Task<TokenReponseDTO> GenerateAndSaveTokens(User user);
        Task<bool> LogoutService(int userId,string token);
        Task RevokeTokenByUserIdAndTokenTypeService(int userId, string token, PublicEnum.TokenType type);
        Task RevokeAllTokensByUserIdService(int userId);
    }
}
