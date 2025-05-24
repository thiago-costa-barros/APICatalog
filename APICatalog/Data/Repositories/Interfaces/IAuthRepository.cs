using APICatalog.API.DTOs;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<TokenReponseDTO?> AuthenticateAsync(LoginRequestDTO loginRequest);
        Task<bool> ValidateTokenAsync(string token);
    }
}
