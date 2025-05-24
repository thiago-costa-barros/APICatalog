using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APICatalog.Core.Services.Helpers
{
    public class TokenServiceHelper : ITokenServiceHelper
    {
        private readonly IConfiguration _config;
        public TokenServiceHelper(IConfiguration config)
        {
            _config = config;
        }
        public TokenReponseDTO GenerateToken(User user)
        {
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                new Claim("IsActive", user.IsActive.ToString()),
                new Claim("Type", user.Type.ToString())
            };
            var secretKey = _config["Jwt:SecretKey"] ?? throw new ArgumentNullException(nameof(_config), "SecretKey cannot be null.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpirationInMinutes"]));

            var accessToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: creds
            );

            var response = new TokenReponseDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = Guid.NewGuid().ToString(),
                ExpirationDate = accessToken.ValidTo,
                UserId = user.UserId
            };


            return response;
        }

        public bool VerifyPasswordHaser(string hashedPassword, string requestPassword)
        {
            return BCrypt.Net.BCrypt.Verify(hashedPassword, requestPassword);
        }
    }
}
