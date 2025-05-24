using APICatalog.API.DTOs;
using APICatalog.Data.Repositories.Interfaces;

namespace APICatalog.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public Task<TokenReponseDTO?> AuthenticateAsync(LoginRequestDTO loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
