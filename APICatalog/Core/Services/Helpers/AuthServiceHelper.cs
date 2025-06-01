using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Data.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APICatalog.Core.Services.Helpers
{
    public class AuthServiceHelper : IAuthServiceHelper
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepository;
        public AuthServiceHelper(IConfiguration config, IAuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;
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
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:AccessTokenExpirationMinutes"]));

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

        public string HashToken(string token)
        {
            return BCrypt.Net.BCrypt.HashPassword(token);
        }

        public bool VerifyPasswordHasher(string requestPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(requestPassword, hashedPassword);
        }

        public bool VerifyTokenHasher(string requestToken, string hashedToken)
        {
            return BCrypt.Net.BCrypt.Verify(requestToken, hashedToken);
        }
    }
}
