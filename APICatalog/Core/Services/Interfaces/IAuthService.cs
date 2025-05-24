using APICatalog.API.DTOs;

namespace APICatalog.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenReponseDTO> AuthenticateAsync(LoginRequestDTO loginRequest);
        Task<bool> ValidateTokenAsync(string token);
    }
}
