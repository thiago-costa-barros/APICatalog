using APICatalog.API.DTOs;

namespace APICatalog.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenReponseDTO> LoginService(LoginRequestDTO loginRequestDTO);
        Task<TokenValidationResultDTO> ValidateTokenService(string token);
        Task<TokenReponseDTO> RefreshTokenService(string token);
        Task<bool> LogoutService(int userId,string token);
    }
}
