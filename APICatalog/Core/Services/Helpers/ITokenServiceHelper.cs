using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.Core.Services.Helpers
{
    public interface ITokenServiceHelper
    {
        bool VerifyPasswordHaser(string hashedPassword, string requestPassword);
        TokenReponseDTO GenerateToken(User user);
    }
}
