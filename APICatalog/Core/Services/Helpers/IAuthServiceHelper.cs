using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.Core.Services.Helpers
{
    public interface IAuthServiceHelper
    {
        string HashToken(string token);
        bool VerifyPasswordHasher(string requestPassword, string hashedPassword);
        bool VerifyTokenHasher(string requestToken, string hashedToken);
        TokenReponseDTO GenerateToken(User user);
    }
}
