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
            var requestTime = DateTime.UtcNow;

            // Identificadores únicos
            var accessTokenIdentifier = Guid.NewGuid();
            var refreshTokenIdentifier = Guid.NewGuid();

            // Geração dos tokens
            var accessToken = GenerateAccessToken(user, accessTokenIdentifier, requestTime);
            var refreshToken = GenerateRefreshToken(user, refreshTokenIdentifier, requestTime);

            TokenReponseDTO response = new TokenReponseDTO
            {
                UserId = user.UserId,
                RequestTime = requestTime,
                AccessToken = new AccessTokenResponseDTO
                {
                    AccessIdentifier = accessTokenIdentifier,
                    AccessToken = accessToken.Token,
                    ExpirationDate = accessToken.Expiration
                },
                RefreshToken = new RefreshTokenResponseDTO
                {
                    RefreshIdentifier = refreshTokenIdentifier,
                    RefreshToken = refreshToken.Token,
                    ExpirationDate = refreshToken.Expiration
                }
            };


            return response;
        }
        private (string Token, DateTime Expiration) GenerateAccessToken(User user, Guid jti, DateTime now)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                new Claim("IsActive", user.IsActive.ToString()),
                new Claim("Type", user.Type.ToString()),
                new Claim("typ", "access")
            };

            var expiration = now.AddMinutes(Convert.ToDouble(_config["Jwt:AccessTokenExpirationMinutes"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }

        private (string Token, DateTime Expiration) GenerateRefreshToken(User user, Guid jti, DateTime now)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()),
                new Claim("typ", "refresh")
            };

            var expiration = now.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpirationDays"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:RefreshKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
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
